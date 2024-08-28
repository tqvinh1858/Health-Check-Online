using BHEP.Infrastructure.VnPay.DependencyInjection.Options.Response;

namespace BHEP.Infrastructure.VnPay.Respository.IRepository;
public interface IVnPayRepository
{
    public string CreatePaymentUrl(decimal amount, string orderInfo, string txnRef, string ipAddress);

}
