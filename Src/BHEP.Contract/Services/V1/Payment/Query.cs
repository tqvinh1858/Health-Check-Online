using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Payment;
public static class Query 
{
    public record GetPaymentByIdQuery(int Id) : IQuery<Responses.PaymentResponse>;

   
}
