namespace BHEP.Domain.Exceptions;
public class VoucherException
{
    public class VoucherBadRequestException : BadRequestException
    {
        public VoucherBadRequestException(string message) : base(message)
        {
        }
    }

    public class VoucherIdNotFoundException : NotFoundException
    {
        public VoucherIdNotFoundException()
            : base($"Voucher not found.") { }
    }
}
