using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Device;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Device.Commands;
public sealed class UpdateDeviceCommandHandler : ICommandHandler<Command.UpdateDeviceCommand, Responses.DeviceResponse>
{
    private readonly IDeviceRepository deviceRepository;
    private readonly IMapper mapper;
    private readonly IProductRepository productRepository;

    public UpdateDeviceCommandHandler(
        IDeviceRepository deviceRepository,
        IMapper mapper,
        IProductRepository productRepository)
    {
        this.deviceRepository = deviceRepository;
        this.mapper = mapper;
        this.productRepository = productRepository;
    }

    public async Task<Result<Responses.DeviceResponse>> Handle(Command.UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceExist = await deviceRepository.FindByIdAsync(request.Id, cancellationToken)
        ?? throw new DeviceException.DeviceIdNotFoundException();

        var productExist = await productRepository.FindByIdAsync(request.ProductId, cancellationToken)
            ?? throw new ProductException.ProductIdNotFoundException();

        try
        {
            deviceExist.Update(
               request.ProductId,
               request.TransactionId,
               request.Code,
               request.IsSale,
               request.SaleDate
               );

            deviceRepository.Update(deviceExist);

            var resultResponse = mapper.Map<Responses.DeviceResponse>(deviceExist);
            return Result.Success(resultResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
