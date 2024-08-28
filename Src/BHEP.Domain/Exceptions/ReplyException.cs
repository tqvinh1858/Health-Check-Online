namespace BHEP.Domain.Exceptions;
public static class ReplyException
{
    public class ReplyBadRequestException : BadRequestException
    {
        public ReplyBadRequestException(string message) : base(message)
        {
        }
    }

    public class ReplyNotFoundException : NotFoundException
    {
        public ReplyNotFoundException()
            : base($"Reply not found.") { }
    }
}
