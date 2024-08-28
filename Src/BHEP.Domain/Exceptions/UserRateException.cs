namespace BHEP.Domain.Exceptions;
public static class UserRateException
{
    public class UserRateBadRequestException : BadRequestException
    {
        public UserRateBadRequestException(string message) : base(message)
        {
        }
    }

    public class UserRateIdNotFoundException : NotFoundException
    {
        public UserRateIdNotFoundException()
            : base($"Rating not found.") { }
    }
}
