using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Schedule;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;


namespace BHEP.Application.Usecases.V1.Schedule.Queries;
public sealed class GetScheduleByIdQueryHandler : IQueryHandler<Query.GetScheduleByIdQuery, Responses.ScheduleResponse>
{
    private readonly IScheduleRepository scheduleRepository;

    public GetScheduleByIdQueryHandler(IScheduleRepository scheduleRepository)
    {
        this.scheduleRepository = scheduleRepository;
    }

    public async Task<Result<Responses.ScheduleResponse>> Handle(Query.GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var listSchedules = await scheduleRepository.GetScheduleByEmployeeId(request.EmployeeId);
        if (!listSchedules.Any() || listSchedules == null)
            throw new ScheduleException.ScheduleNotFoundException();
        var response = scheduleRepository.ConvertToResponse(listSchedules);

        return Result.Success(response);
    }
}
