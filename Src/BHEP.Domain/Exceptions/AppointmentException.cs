namespace BHEP.Domain.Exceptions;
public static class AppointmentException
{
    public class AppointmentBadRequestException : BadRequestException
    {
        public AppointmentBadRequestException(string message) : base(message)
        {
        }
    }

    public class AppointmentIdNotFoundException : NotFoundException
    {
        public AppointmentIdNotFoundException()
            : base($"Appointment not found.") { }
    }

    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string message)
            : base(message) { }
    }
}
