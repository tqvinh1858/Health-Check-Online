using BHEP.Contract.Abstractions.Message;
using static BHEP.Contract.Services.V1.Schedule.Responses;

namespace BHEP.Contract.Services.V1.Schedule;
public static class Command
{
    public record CreateScheduleCommand(
        int EmployeeId,
        List<Schedules> Schedules) : ICommand<Responses.ScheduleResponse>;

    public record Schedules(
        string Date,
        string[] Time);


    public record UpdateScheduleCommand(
        int EmployeeId,
        string Date,
        string[] Time) : ICommand<Responses.ScheduleResponse>;



    public record DeleteScheduleCommand(int ScheduleId, int EmployeeId) : ICommand;
}
