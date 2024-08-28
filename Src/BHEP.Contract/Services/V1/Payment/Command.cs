using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;
using Net.payOS.Types;
using static BHEP.Contract.Services.V1.Payment.Responses;

namespace BHEP.Contract.Services.V1.Payment;
public static class Command
{
    public record CreatePaymentCommand(
        int UserId,
        decimal Amount) : ICommand<Responses.PaymentResponse>;


    public record UpdatePaymentCommand(
       int? Id,
       PaymentStatus Status) : ICommand<Responses.PaymentResponse>;


    public record CreatePayOSLinkCommand(
        int UserId,
        int Amount,
        string Description,
        List<ItemData> items,
        string returnUrl,
        string cancelUrl,
        int? ExpiredAt) : ICommand<PayOSResponse>;

    public record DeletePaymentCommand(int Id) : ICommand;

}
