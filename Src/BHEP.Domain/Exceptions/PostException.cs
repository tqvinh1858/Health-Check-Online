namespace BHEP.Domain.Exceptions;
public static class PostException
{
    public class PostBadRequestException : BadRequestException
    {
        public PostBadRequestException(string message) : base(message)
        {
        }
    }

    public class PostNotFoundException : NotFoundException
    {
        public PostNotFoundException()
            : base($"Post not found.") { }
    }

}
