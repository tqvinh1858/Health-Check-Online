using System.Globalization;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.HealthRecord;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.HealthRecord.Queries;
public sealed class GetHealthRecordByUserIdQueryHandler : IQueryHandler<Query.GetHealthRecordByUserIdQuery, Responses.HealthRecordResponse>
{
    private readonly IHealthRecordRepository healthRecordRepository;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    public GetHealthRecordByUserIdQueryHandler(
        IHealthRecordRepository healthRecordRepository,
        IUserRepository userRepository,
        IMapper mapper) 
    {
        this.healthRecordRepository = healthRecordRepository;
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.HealthRecordResponse>> Handle(Query.GetHealthRecordByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId)
            ?? throw new HealthRecordException.UserIdNotFoundException();

        var EventsQuery = healthRecordRepository.FindAll();

        EventsQuery = request.DeviceId == null ? EventsQuery : EventsQuery.Where(x => x.DeviceId == request.DeviceId && x.UserId == request.UserId);

        if (!string.IsNullOrEmpty(request.CreatedDate))
        {
            if (!DateTime.TryParseExact(request.CreatedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime scheduleDate))
                throw new HealthRecordException.HealthRecordBadRequestException("Invalid Date Format");
            EventsQuery = EventsQuery.Where(x => x.CreatedDate.Date == scheduleDate.Date);
        }

        var Events = EventsQuery.OrderBy(x => x.Id).OrderBy(x => x.CreatedDate).ToList();

        List<Responses.HealthParamResponse> healthParams = new List<Responses.HealthParamResponse>();
        foreach (var item in Events)
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
}
