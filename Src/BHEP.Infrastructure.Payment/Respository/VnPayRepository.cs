using BHEP.Contract.Constants;
using BHEP.Infrastructure.VnPay.DependencyInjection.Options.Config;
using BHEP.Infrastructure.VnPay.DependencyInjection.Options.Request;
using BHEP.Infrastructure.VnPay.Respository.IRepository;
using Microsoft.Extensions.Options;

namespace BHEP.Infrastructure.VnPay.Respository;
public class VnPayRepository : IVnPayRepository
{
    private readonly VnPayConfig _config;
    private readonly IOptions<VnPayConfig> configOptions;

    public VnPayRepository(IOptions<VnPayConfig> configOptions)
    {
        _config = configOptions?.Value ?? throw new ArgumentNullException(nameof(configOptions));
    }


    public string CreatePaymentUrl(decimal amount, string orderInfo, string txnRef, string ipAddress)
    {
        var createDate = TimeZones.SoutheastAsia;
        var vnpayRequest = new VnPayRequest(
            _config.Version,
            _config.TmnCode,
            createDate,
            ipAddress,
            amount,
            "VND",
            "other",
            orderInfo,
            _config.ReturnUrl,
            txnRef
        );

        var paymentUrl = vnpayRequest.GetLink(_config.PaymentUrl, _config.HashSecret);
        return paymentUrl;
    }



}
