namespace BHEP.Domain.Exceptions;
public static class SpecialistException
{
    public class SpecialistBadRequestException : BadRequestException
    {
        public SpecialistBadRequestException(string message) : base(message)
        {
        }
    }

    public class SpecialistIdNotFoundException : NotFoundException
    {
        public SpecialistIdNotFoundException()
            : base($"Specialist not found.") { }
    }
}
