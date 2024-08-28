using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.HealthRecord;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.HealthRecord.Commands;
public sealed class CreateHealthRecordCommandHandler : ICommandHandler<Command.CreateHealthRecordCommand, Responses.HealthRecordResponse>
{
    private readonly IHealthRecordRepository healthRecordRepository;
    private readonly IUserRepository userRepository;
    private readonly IDeviceRepository deviceRepository;
    private readonly IUnitOfWork unitOfWork;
    public CreateHealthRecordCommandHandler(
        IHealthRecordRepository healthRecordRepository,
        IUserRepository userRepository,
        IDeviceRepository deviceRepository,
        IUnitOfWork unitOfWork)
    {
        this.healthRecordRepository = healthRecordRepository;
        this.userRepository = userRepository;
        this.deviceRepository = deviceRepository;
        this.unitOfWork = unitOfWork;
    }


    public async Task<Result<Responses.HealthRecordResponse>> Handle(Command.CreateHealthRecordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId)
            ?? throw new HealthRecordException.UserIdNotFoundException();

        var deviceId = await deviceRepository.FindByIdAsync(request.DeviceId)
            ?? throw new HealthRecordException.DeviceIdNotFoundException();

        try
        {
            var listHealthRecord = new List<Domain.Entities.UserEntities.HealthRecord>();
            foreach (var parameter in request.HealthParams)
            {
                var healthRecord = new Domain.Entities.UserEntities.HealthRecord
                {
                    UserId = request.UserId,
                    DeviceId = request.DeviceId,
                    Temp = parameter.Temp,
                    HeartBeat = parameter.HeartBeat,
                    ESpO2 = parameter.ESpO2,
                    CreatedDate = TimeZones.SoutheastAsia,
                };

                listHealthRecord.Add(healthRecord);
            }

            healthRecordRepository.AddMultiple(listHealthRecord);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            List<Responses.HealthParamResponse> healthParams = new List<Responses.HealthParamResponse>();
            foreach (var item in listHealthRecord)
            {
                healthParams.Add(new Responses.HealthParamResponse(
                    item.Id,
                    item.DeviceId,
                    item.Temp,
                    item.HeartBeat,
                    item.ESpO2,
                    item.CreatedDate.ToString("dd-MM-yyyy")));
            }

            var result = new Responses.HealthRecordResponse(
                request.UserId,
                healthParams);

            return Result.Success(result);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex);
        }
    }
}
