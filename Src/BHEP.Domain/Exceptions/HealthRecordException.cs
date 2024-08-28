namespace BHEP.Domain.Exceptions;
public static class HealthRecordException
{
    public class HealthRecordBadRequestException : BadRequestException
    {
        public HealthRecordBadRequestException(string message) : base(message)
        {
        }
    }

    public class UserIdNotFoundException : NotFoundException
    {
        public UserIdNotFoundException()
            : base($"User not found.") { }
    }

    public class DeviceIdNotFoundException : NotFoundException
    {
        public DeviceIdNotFoundException()
            : base($"Device not found.") { }
    }
}
