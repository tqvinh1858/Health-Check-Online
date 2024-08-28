namespace BHEP.Contract.Services.V1.Voucher;
public class Responses
{
    public record VoucherResponse(
       int Id,
       string Name,
       string Code,
       float Discount,
       int Stock,
       bool IsExpired,
       bool OutOfStock);

}
