namespace BHEP.Domain.Exceptions;
public static class BlogException
{
    public class BlogBadRequestException : BadRequestException
    {
        public BlogBadRequestException(string message) : base(message)
        {
        }
    }

    public class BlogIdNotFoundException : NotFoundException
    {
        public BlogIdNotFoundException()
            : base($"Blog not found.") { }
    }
}

