namespace BHEP.Domain.Exceptions;
public static class ServiceException
{
    public class ServiceBadRequestException : BadRequestException
    {
        public ServiceBadRequestException(string message) : base(message)
        {
        }
    }
    public class ServiceIdNotFoundException : NotFoundException
    {
        public ServiceIdNotFoundException()
            : base($"Service not found.") { }
    }

}
