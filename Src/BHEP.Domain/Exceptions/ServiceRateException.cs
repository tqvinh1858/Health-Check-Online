namespace BHEP.Domain.Exceptions;
public static class ServiceRateException
{
    public class ServiceRateBadRequestException : BadRequestException
    {
        public ServiceRateBadRequestException(string message) : base(message)
        {
        }
    }

    public class ServiceRateIdNotFoundException : NotFoundException
    {
        public ServiceRateIdNotFoundException()
            : base($"Rating not found.") { }
    }
}
