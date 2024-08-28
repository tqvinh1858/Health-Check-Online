namespace BHEP.Domain.Exceptions;
public static class ProductException
{
    public class ProductBadRequestException : BadRequestException
    {
        public ProductBadRequestException(string message) : base(message)
        {
        }
    }

    public class ProductIdNotFoundException : NotFoundException
    {
        public ProductIdNotFoundException()
            : base($"Product not found.") { }
    }
}
