using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Device;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Device.Commands;
public sealed class DeleteDeviceCommandHandler : ICommandHandler<Command.DeleteDeviceCommand>
{
    private readonly IDeviceRepository deviceRepository;

    public DeleteDeviceCommandHandler(IDeviceRepository deviceRepository)
    {
        this.deviceRepository = deviceRepository;
    }

    public async Task<Result> Handle(Command.DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceExist = await deviceRepository.FindByIdAsync(request.Id, cancellationToken)
                       ?? throw new DeviceException.DeviceIdNotFoundException();

        try
        {
            deviceRepository.Remove(deviceExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
