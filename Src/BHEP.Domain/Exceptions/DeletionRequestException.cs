namespace BHEP.Domain.Exceptions;
public static class DeletionRequestException 
{
    public class DeletionRequestBadRequestException : BadRequestException
    {
        public DeletionRequestBadRequestException(string message) : base(message)
        {
        }
    }


    public class DeletionRequestIdNotFoundException : NotFoundException
    {
        public DeletionRequestIdNotFoundException()
            : base($"Deletion Request not found.") { }
    }

    public class UserIdNotFoundException : NotFoundException
    {
        public UserIdNotFoundException()
            : base($"User not found.") { }
    }
}
