using BHEP.Infrastructure.PayOS.DependencyInjection.Options;
using BHEP.Infrastructure.PayOS.Repository.IRepository;
using Microsoft.Extensions.Options;
using Net.payOS.Types;

namespace BHEP.Infrastructure.PayOS.Repository;
public class PayOSRepository : IPayOSRepository
{
    private readonly PayOSOptions payOSOptions;
    private readonly Net.payOS.PayOS payOS;
    public PayOSRepository(IOptions<PayOSOptions> PayOSOptions)
    {
        payOSOptions = PayOSOptions.Value;
        payOS = new Net.payOS.PayOS(
            payOSOptions.ClientID ?? throw new Exception("Cannot find ClientID of PayOS"),
            payOSOptions.APIKey ?? throw new Exception("Cannot find APIKey of PayOS"),
            payOSOptions.CheckSumKey ?? throw new Exception("Cannot find CheckSumKey of PayOS"));
    }

    public async Task<CreatePaymentResult> CreatePaymentLink(PaymentData paymentData)
    {
        try
        {
            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);

            return createPayment;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task cancelPaymentLink(int orderId)
    {
        try
        {
            PaymentLinkInformation paymentLinkInformation = await payOS.cancelPaymentLink(orderId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
