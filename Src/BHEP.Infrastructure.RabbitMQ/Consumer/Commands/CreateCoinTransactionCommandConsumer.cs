using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V2.CoinTransaction;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V2.Consumers.Commands;

public class CreateCoinTransactionCommandConsumer : IConsumer<Command.CreateCoinTransactionCommand>
{
    private readonly ICodeRepository codeRepository;
    private readonly IUserCodeRepository userCodeRepository;
    private readonly IDurationRepository durationRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;
    private readonly IDeviceRepository deviceRepository;
    private readonly IServiceRepository serviceRepository;
    private readonly IVoucherRepository voucherRepository;
    private readonly IProductRepository productRepository;
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly ApplicationDbContext dbcontext;
    private const int SpiritDeviceId = 1;

    public CreateCoinTransactionCommandConsumer(
        ICodeRepository codeRepository,
        IUserCodeRepository userCodeRepository,
        IDurationRepository durationRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IDeviceRepository deviceRepository,
        IServiceRepository serviceRepository,
        IVoucherRepository voucherRepository,
        IProductRepository productRepository,
        ApplicationDbContext dbcontext,
        ICoinTransactionRepository coinTransactionRepository)
    {
        this.codeRepository = codeRepository;
        this.userCodeRepository = userCodeRepository;
        this.durationRepository = durationRepository;
        this.unitOfWork = unitOfWork;
        this.userRepository = userRepository;
        this.deviceRepository = deviceRepository;
        this.serviceRepository = serviceRepository;
        this.voucherRepository = voucherRepository;
        this.productRepository = productRepository;
        this.dbcontext = dbcontext;
        this.coinTransactionRepository = coinTransactionRepository;
    }
    public async Task Consume(ConsumeContext<Command.CreateCoinTransactionCommand> context)
    {
        var request = context.Message;
        var user = await userRepository.FindByIdAsync(request.UserId)
            ?? throw new UserException.UserNotFoundException();

        // Check Balance enough?
        if (user.Balance < request.Amount)
            throw new CoinTransactionException.CoinTransactionBadRequestException("Balance not enough");
        var strategy = dbcontext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbcontext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            {
                try
                {
                    user.Balance -= request.Amount;
                    // Add Transaction
                    var cointransaction = new Domain.Entities.SaleEntities.CoinTransaction
                    {
                        UserId = request.UserId,
                        Amount = request.Amount,
                        IsMinus = request.IsMinus,
                        Title = request.Title,
                        Description = request.Description,
                        Type = CoinTransactionType.Order
                    };
                    coinTransactionRepository.Add(cointransaction);
                    await unitOfWork.SaveChangesAsync();

                    // HandleItem
                    await HandleVouchers(request.Vouchers, cointransaction.Id);
                    await HandleProducts(request.Products, cointransaction.Id);
                    string FamilyCode = await HandleService(request, cointransaction.Id);

                    await unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (BadRequestException be)
                {
                    throw new CoinTransactionException.CoinTransactionBadRequestException(be.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        });
    }

    private async Task HandleVouchers(List<int> vouchers, int transactionId)
    {
        if (vouchers != null && vouchers.Count > 0)
        {
            foreach (var voucherId in vouchers)
            {
                var voucher = await voucherRepository.FindByIdAsync(voucherId)
                    ?? throw new CoinTransactionException.CoinTransactionBadRequestException("Voucher Not Found");
                if (voucher.IsExpired)
                    throw new CoinTransactionException.CoinTransactionBadRequestException("Voucher is Expired");
                else if (voucher.OutOfStock)
                    throw new CoinTransactionException.CoinTransactionBadRequestException("Voucher is OutOfStock");

                dbcontext.VoucherTransaction.Add(new Domain.Entities.SaleEntities.VoucherTransaction
                {
                    VoucherId = voucherId,
                    TransactionId = transactionId
                });
                --voucher.Stock;
            }
        }
    }

    private async Task HandleProducts(List<Command.ProductTransaction> products, int transactionId)
    {
        if (products != null && products.Count > 0)
        {
            foreach (var productTransaction in products)
            {
                var product = await productRepository.FindByIdAsync(productTransaction.Id)
                    ?? throw new CoinTransactionException.CoinTransactionBadRequestException("Product Not Found");
                if (product.SoldOut)
                    throw new CoinTransactionException.CoinTransactionBadRequestException("Product is SoldOut");
                if (productTransaction.Quantity > product.Stock)
                    throw new CoinTransactionException.CoinTransactionBadRequestException("Out of Stock");

                // Create DeviceName
                if (product.Id == SpiritDeviceId)
                {
                    var devices = await deviceRepository
                        .FindAll(x => x.IsSale == false)
                        .Take(productTransaction.Quantity)
                        .ToListAsync();

                    devices.ForEach(x =>
                    {
                        x.IsSale = true;
                        x.TransactionId = transactionId;
                        x.SaleDate = TimeZones.SoutheastAsia;
                    });

                    deviceRepository.UpdateMultiple(devices);
                }

                dbcontext.ProductTransaction.Add(new Domain.Entities.SaleEntities.ProductTransaction
                {
                    ProductId = productTransaction.Id,
                    TransactionId = transactionId,
                    Quantity = productTransaction.Quantity
                });

                product.Stock -= productTransaction.Quantity;
                productRepository.Update(product);
            }
        }

    }
    private async Task<string?> HandleService(Command.CreateCoinTransactionCommand request, int transactionId)
    {
        string FamilyCode = string.Empty;
        if (request.serviceId == null)
            return FamilyCode;

        var service = await serviceRepository.FindByIdAsync(request.serviceId.Value)
            ?? throw new CoinTransactionException.CoinTransactionBadRequestException("Service not found");

        dbcontext.ServiceTransaction.Add(new Domain.Entities.SaleEntities.ServiceTransaction
        {
            ServiceId = request.serviceId.Value,
            TransactionId = transactionId
        });

        if (service.Type == Contract.Enumerations.ServiceType.Family)
        {
            if (request.IsGenerateCode)
                FamilyCode = await GenerateCode(request.UserId, transactionId);
            else if (!string.IsNullOrEmpty(request.Code))
                await UseCode(request.UserId, request.Code);
        }

        var duration = new Domain.Entities.SaleEntities.Duration
        {
            ServiceId = service.Id,
            UserId = request.UserId,
            StartDate = TimeZones.SoutheastAsia,
            EndDate = TimeZones.SoutheastAsia.AddMonths((int)service.Duration)
        };
        durationRepository.Add(duration);

        return FamilyCode;
    }

    private async Task<string> GenerateCode(int userId, int transactionId)
    {
        string FamilyCode;
        do
        {
            FamilyCode = RandomString(10);
        } while (await codeRepository.FindAll(x => x.Name == FamilyCode).AnyAsync());

        var code = new Domain.Entities.SaleEntities.Code
        {
            Name = FamilyCode,
            StartDate = TimeZones.SoutheastAsia,
            EndDate = TimeZones.SoutheastAsia.AddYears(1),
            UserId = userId,
            TransactionId = transactionId
        };

        codeRepository.Add(code);
        await unitOfWork.SaveChangesAsync();

        await userCodeRepository.Add(new Domain.Entities.SaleEntities.UserCode
        {
            UserId = userId,
            CodeId = code.Id
        });

        return FamilyCode;
    }
    private async Task UseCode(int userId, string FamilyCode)
    {
        var code = await codeRepository.FindAll(x => x.Name == FamilyCode).FirstOrDefaultAsync()
            ?? throw new AppointmentException.AppointmentBadRequestException("Code Not Exist");

        await userCodeRepository.Add(new Domain.Entities.SaleEntities.UserCode
        {
            UserId = userId,
            CodeId = code.Id
        });
    }
    private static string RandomString(int length)
    {
        Random random = new Random();

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
