using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Schedule;
public static class Query
{
    public record GetScheduleByIdQuery(int EmployeeId) : IQuery<Responses.ScheduleResponse>;

    public record GetScheduleByDateQuery(string Date) : IQuery<Responses.ScheduleResponse>;
}
