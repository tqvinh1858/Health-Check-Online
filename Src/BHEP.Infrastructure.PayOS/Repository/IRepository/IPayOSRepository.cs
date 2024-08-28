using Net.payOS.Types;

namespace BHEP.Infrastructure.PayOS.Repository.IRepository;
public interface IPayOSRepository
{
    Task<CreatePaymentResult> CreatePaymentLink(PaymentData paymentData);
    Task cancelPaymentLink(int orderId);
}
