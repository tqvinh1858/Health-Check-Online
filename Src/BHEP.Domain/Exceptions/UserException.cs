namespace BHEP.Domain.Exceptions;
public static class UserException
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException(string message) : base(message)
        {
        }
    }

    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
            : base($"Customer not found.") { }
    }
}
