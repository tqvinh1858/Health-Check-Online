namespace BHEP.Domain.Exceptions;
public static class PaymentException
{
    public class PaymentBadRequestException : BadRequestException
    {
        public PaymentBadRequestException(string message) : base(message)
        {
        }
    }

    public class PaymentIdNotFoundException : NotFoundException
    {
        public PaymentIdNotFoundException()
            : base($"Payment not found.") { }
    }

}
