using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.Device;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Device.Commands;
public sealed class CreateDeviceCommandHandler : ICommandHandler<Command.CreateDeviceCommand>
{
    private readonly IDeviceRepository deviceRepository;
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;
    private const int SpiritId = 1;
    private readonly Random random = new Random();

    public CreateDeviceCommandHandler(
        IDeviceRepository deviceRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        this.deviceRepository = deviceRepository;
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity < 1)
            throw new DeviceException.DeviceBadRequestException("Quantity At Least 1");
        var product = await productRepository.FindByIdAsync(SpiritId)
               ?? throw new ProductException.ProductIdNotFoundException();
        try
        {
            var listNewDevices = new List<Domain.Entities.SaleEntities.Device>();
            for (int i = 0; i < request.Quantity; i++)
            {
                var device = new Domain.Entities.SaleEntities.Device
                {
                    ProductId = SpiritId,
                    Code = GenerateCode(),
                    IsSale = false
                };
                listNewDevices.Add(device);
            }
            deviceRepository.AddMultiple(listNewDevices);
            await unitOfWork.SaveChangesAsync();

            product.Stock = deviceRepository.FindAll(x => x.IsSale == false).Count();
            productRepository.Update(product);

            return Result.Success(201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private string GenerateCode()
    {
        return $"{TimeZones.SoutheastAsia.ToString("ddMMyyyyHHmmssfff")}{random.NextInt64(1000, 9999)}";
    }
}
