namespace BHEP.Domain.Exceptions;
public static class WorkProfileException
{
    public class WorkProfileBadRequestException : BadRequestException
    {
        public WorkProfileBadRequestException(string message) : base(message)
        {
        }
    }

    public class WorkProfileIdNotFoundException : NotFoundException
    {
        public WorkProfileIdNotFoundException()
            : base($"WorkProfile not found.") { }
    }

    public class UserIdNotFoundException : NotFoundException
    {
        public UserIdNotFoundException()
            : base($"User not found.") { }
    }
}
