namespace BHEP.Contract.Services.V1.Payment;
public static class Responses
{
    public record PaymentResponse
    {
        public PaymentResponse(int id, int userId, string method, string paymentUrl)
        {
            Id = id;
            UserId = userId;
            Method = method;
            PaymentUrl = paymentUrl;
        }

        public PaymentResponse()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Method { get; set; }
        public string PaymentUrl { get; set; }

    }


    public class PaymentReturnDtos
    {
        public string? PaymentId { get; set; }
        /// <summary>
        /// 00: Success
        /// 99: Unknown
        /// 10: Error
        /// </summary>
        public string? PaymentStatus { get; set; }
        public string? PaymentMessage { get; set; }
        /// <summary>
        /// Format: yyyyMMddHHmmss
        /// </summary>
        public string? PaymentDate { get; set; }
        public string? PaymentRefId { get; set; }
        public decimal? Amount { get; set; }
        public string? Signature { get; set; }
    }
  
    public record PayOSResponse(
        int id,
        string accountNumber,
        int amount,
        string description,
        long orderCode,
        string currency,
        string paymentLinkId,
        string status,
        string checkoutUrl,
        string qrCode);

}
