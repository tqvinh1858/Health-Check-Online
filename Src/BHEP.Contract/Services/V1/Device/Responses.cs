using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Device;
public class Responses
{
    public record DeviceResponse(
       int Id,
       int ProductId,
       int? TransactionId,
       string Code,
       bool IsSale,
       DateTime CreatedDate,
       DateTime? SaleDate
       );


    

}
