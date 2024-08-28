namespace BHEP.Domain.Exceptions;
public static class SymptomException
{
    public class SymptomBadRequestException : BadRequestException
    {
        public SymptomBadRequestException(string message) : base(message)
        {
        }
    }

    public class SymptomIdNotFoundException : NotFoundException
    {
        public SymptomIdNotFoundException()
            : base($"Symptom not found.") { }
    }
}
