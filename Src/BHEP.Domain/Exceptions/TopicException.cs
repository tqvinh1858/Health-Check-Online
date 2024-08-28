namespace BHEP.Domain.Exceptions;
public static class TopicException
{
    public class TopicBadRequestException : BadRequestException
    {
        public TopicBadRequestException(string message) : base(message)
        {
        }
    }

    public class TopicIdNotFoundException : NotFoundException
    {
        public TopicIdNotFoundException()
            : base($"Topic not found.") { }
    }
}

