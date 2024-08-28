namespace BHEP.Domain.Exceptions;
public static class ScheduleException
{
    public class ScheduleBadRequestException : BadRequestException
    {
        public ScheduleBadRequestException(string message) : base(message)
        {
        }
    }

    public class ScheduleNotFoundException : NotFoundException
    {
        public ScheduleNotFoundException()
            : base($"Schedule not found.") { }
    }

    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException()
            : base($"Employee not found.") { }
    }

    public class EmployeeNotAuthorizedException : UnauthorizedAccessException
    {
        public EmployeeNotAuthorizedException()
            : base("Employee is not authorized to delete this schedule.") { }
    }
}
