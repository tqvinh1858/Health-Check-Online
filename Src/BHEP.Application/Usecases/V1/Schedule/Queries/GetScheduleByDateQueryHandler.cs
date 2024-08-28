using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Schedule;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Schedule.Queries;
public sealed class GetScheduleByDateQueryHandler : IQueryHandler<Query.GetScheduleByDateQuery, Responses.ScheduleResponse>
{
    private readonly IScheduleRepository scheduleRepository;

    public GetScheduleByDateQueryHandler(IScheduleRepository scheduleRepository)
    {
        this.scheduleRepository = scheduleRepository;
    }

    public async Task<Result<Responses.ScheduleResponse>> Handle(Query.GetScheduleByDateQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var listSchedules = await scheduleRepository.GetScheduleByDate(request.Date);
            if (listSchedules == null || !listSchedules.Any())
                return Result.Failure<Responses.ScheduleResponse>("Schedule not found", 404);


            var response = scheduleRepository.ConvertToResponse(listSchedules);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return Result.Failure<Responses.ScheduleResponse>(ex.Message);
        }

    }
}
