namespace BHEP.Domain.Exceptions;
public static class ProductRateException
{
    public class ProductRateBadRequestException : BadRequestException
    {
        public ProductRateBadRequestException(string message) : base(message)
        {
        }
    }

    public class ProductRateIdNotFoundException : NotFoundException
    {
        public ProductRateIdNotFoundException()
            : base($"Rating not found.") { }
    }
}
