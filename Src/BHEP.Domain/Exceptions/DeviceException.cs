namespace BHEP.Domain.Exceptions;
public static class DeviceException 
{
    public class DeviceBadRequestException : BadRequestException
    {
        public DeviceBadRequestException(string message) : base(message)
        {
        }
    }

    public class DeviceIdNotFoundException : NotFoundException
    {
        public DeviceIdNotFoundException()
            : base($"Device not found.") { }
    }
}
