namespace BHEP.Domain.Exceptions;
public static class BlogRateException
{
    public class BlogRateBadRequestException : BadRequestException
    {
        public BlogRateBadRequestException(string message) : base(message)
        {
        }
    }

    public class BlogRateIdNotFoundException : NotFoundException
    {
        public BlogRateIdNotFoundException()
            : base($"Rating not found.") { }
    }
}
