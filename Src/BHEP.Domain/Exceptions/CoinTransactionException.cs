namespace BHEP.Domain.Exceptions;
public class CoinTransactionException
{
    public class CoinTransactionBadRequestException : BadRequestException
    {
        public CoinTransactionBadRequestException(string message) : base(message)
        {
        }
    }

    public class CoinTransactionIdNotFoundException : NotFoundException
    {
        public CoinTransactionIdNotFoundException()
            : base($"CoinTransaction not found.") { }
    }
}
