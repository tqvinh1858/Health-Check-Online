namespace BHEP.Domain.Exceptions;
public static class MajorException {  
    public class MajorBadRequestException : BadRequestException
    {
        public MajorBadRequestException(string message) : base(message)
        {
        }
    }

    public class MajorIdNotFoundException : NotFoundException
    {
        public MajorIdNotFoundException()
            : base($"Major not found.") { }
    }
}
