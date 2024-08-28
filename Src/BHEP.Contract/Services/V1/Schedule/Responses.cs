namespace BHEP.Contract.Services.V1.Schedule;
public static class Responses
{

    public record ScheduleResponse(
        int EmployeeId,
        List<WeekSchedule> WeekSchedule)
    {
        public ScheduleResponse() : this(0, new List<WeekSchedule>()) { }
    }


    public record WeekSchedule(
        int Id,
        string Date,
        string[] Time);


}
