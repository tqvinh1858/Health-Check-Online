namespace BHEP.Domain.Exceptions;
public static class JobApplicationException
{
    public class JobApplicationBadRequestException : BadRequestException
    {
        public JobApplicationBadRequestException(string message) : base(message)
        {
        }
    }

    public class JobApplicationIdNotFoundException : NotFoundException
    {
        public JobApplicationIdNotFoundException()
            : base($"JobApplication not found.") { }
    }
}
