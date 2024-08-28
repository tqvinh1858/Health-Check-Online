using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V3.CoinTransaction;
public static class Responses
{
    public record CoinTransactionResponse()
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsMinus { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string CreatedDate { get; set; }
        public string? FamilyCode { get; set; }
        public List<VoucherResponse>? Vouchers { get; set; }
        public ServiceResponse? Service { get; set; }
        public List<ProductResponse>? Products { get; set; }
        public List<DeviceResponse>? Devices { get; set; }
    }

    public record VoucherResponse(
        int Id,
        string Name,
        string Code,
        float Discount);

    public record ServiceResponse(
        int Id,
        string Name,
        string Image,
        ServiceType Type,
        string Description,
        float Price,
        TimeExpired Duration);

    public record ProductResponse(
       int Id,
       string Name,
       string Image,
       string Description,
       float Price);

    public record DeviceResponse(
        int Id,
        string Code);
}
