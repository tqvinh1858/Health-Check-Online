using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.CoinTransaction;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.CoinTransaction.Commands;
public sealed class CreateCoinTransactionCommandHandler : ICommandHandler<Command.CreateCoinTransactionCommand, Responses.CoinTransactionResponse>
{
    private readonly ICodeRepository codeRepository;
    private readonly IUserCodeRepository userCodeRepository;
    private readonly IDurationRepository durationRepository;
    private readonly ApplicationDbContext context;
    private readonly IUserRepository userRepository;
    private readonly IDeviceRepository deviceRepository;
    private readonly IMapper mapper;
    private const int SpiritDeviceId = 1;

    public CreateCoinTransactionCommandHandler(
        IUserRepository userRepository,
        IUserCodeRepository userCodeRepository,
        IDurationRepository durationRepository,
        ApplicationDbContext context,
        IMapper mapper,
        ICodeRepository codeRepository,
        IDeviceRepository deviceRepository)
    {
        this.userRepository = userRepository;
        this.userCodeRepository = userCodeRepository;
        this.durationRepository = durationRepository;
        this.context = context;
        this.mapper = mapper;
        this.codeRepository = codeRepository;
        this.deviceRepository = deviceRepository;
    }

    public async Task<Result<Responses.CoinTransactionResponse>> Handle(Command.CreateCoinTransactionCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId);

        if (user is null)
            return Result.Failure<Responses.CoinTransactionResponse>("User is not exist");

        //Check Balance enough ?
        if (user.Balance < request.Amount)
            return Result.Failure<Responses.CoinTransactionResponse>("Balance not enough");
        try
        {
            user.Balance -= request.Amount;
            // Add Transaction
            var transaction = new Domain.Entities.SaleEntities.CoinTransaction
            {
                UserId = request.UserId,
                Amount = request.Amount,
                IsMinus = request.IsMinus,
                Title = request.Title,
                Description = request.Description,
                Type = CoinTransactionType.Order
            };
            await context.CoinTransaction.AddAsync(transaction);
            await context.SaveChangesAsync();

            // HandleItem
            await HandleVouchers(request.Vouchers, transaction.Id);
            await HandleProducts(request.Products, transaction.Id);
            string FamilyCode = await HandleService(request, transaction.Id);

            await context.SaveChangesAsync();

            // Convert Response
            var response = new Responses.CoinTransactionResponse
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Amount = transaction.Amount,
                IsMinus = transaction.IsMinus,
                Title = transaction.Title,
                Description = transaction.Description,
                CreatedDate = transaction.CreatedDate.ToString("dd-MM-yyyy"),
                FamilyCode = request.IsGenerateCode ? FamilyCode : request.Code,
                Vouchers = mapper.Map<List<Responses.VoucherResponse>>(transaction.VoucherTransaction
                                                            .Select(x => x.Voucher).ToList()),
                Service = mapper.Map<Responses.ServiceResponse>(transaction.ServiceTransactions
                                                            .Select(x => x.Service).FirstOrDefault()),
                Products = mapper.Map<List<Responses.ProductResponse>>(transaction.ProductTransactions
                                                            .Select(x => x.Product).ToList()),
                Devices = mapper.Map<List<Responses.DeviceResponse>>(transaction.Devices)
            };

            return Result.Success(response, 202);
        }
        catch (BadRequestException be)
        {
            throw new CoinTransactionException.CoinTransactionBadRequestException(be.Message);
        }
        catch (DbUpdateException due)
        {
            throw new CoinTransactionException.CoinTransactionBadRequestException("Product is SoldOut");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    private async Task HandleVouchers(List<int> vouchers, int transactionId)
    {
        if (vouchers != null && vouchers.Count > 0)
        {
            foreach (var voucherId in vouchers)
            {
                var voucher = await context.Voucher.FindAsync(voucherId)
                    ?? throw new CoinTransactionException.CoinTransactionBadRequestException("Voucher Not Found");
                if (voucher.IsExpired)
                    throw new CoinTransactionException.CoinTransactionBadRequestException("Voucher is Expired");
                else if (voucher.OutOfStock)
                    throw new CoinTransactionException.CoinTransactionBadRequestException("Voucher is OutOfStock");

                context.VoucherTransaction.Add(new Domain.Entities.SaleEntities.VoucherTransaction
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
                var product = await context.Product.FindAsync(productTransaction.Id)
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

                    context.Device.UpdateRange(devices);
                }

                context.ProductTransaction.Add(new Domain.Entities.SaleEntities.ProductTransaction
                {
                    ProductId = productTransaction.Id,
                    TransactionId = transactionId,
                    Quantity = productTransaction.Quantity
                });

                product.Stock -= productTransaction.Quantity;
                context.Product.Update(product);
            }
        }

    }
    private async Task<string?> HandleService(Command.CreateCoinTransactionCommand request, int transactionId)
    {
        string FamilyCode = string.Empty;
        if (request.serviceId == null)
            return FamilyCode;

        var service = await context.Service.FindAsync(request.serviceId)
            ?? throw new CoinTransactionException.CoinTransactionBadRequestException("Service not found");

        context.ServiceTransaction.Add(new Domain.Entities.SaleEntities.ServiceTransaction
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
        await context.SaveChangesAsync();

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
