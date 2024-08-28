namespace BHEP.Domain.Exceptions;
public static class AuthException
{
    public class AuthBadRequestException : BadRequestException
    {
        public AuthBadRequestException(string message) : base(message)
        {
        }
    }

    public class AuthIdNotFoundException : NotFoundException
    {
        public AuthIdNotFoundException()
            : base($"User was not found!") { }
    }

    public class AuthEmailNotFoundException : NotFoundException
    {
        public AuthEmailNotFoundException()
            : base($"Email was not register!") { }
    }
}
