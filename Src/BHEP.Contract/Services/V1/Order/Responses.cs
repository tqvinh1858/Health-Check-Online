namespace BHEP.Contract.Services.V1.Order;
public static class Responses
{
    public record OrderResponse(
        int OrderId,
        int UserId,
        int PaymentId,
        int? CodeId,
        DateTime OrderDate,
        float TotalPrice,
        List<OrderServiceDetailDto> OrderServiceDetails,
        List<OrderProductDetailDto> OrderProductDetails
        );

    public record OrderServiceDetailDto(
        int ServiceId,
        string ServiceName,
        float Price);

    public record OrderProductDetailDto(
        int ProductId,
        string ProductName,
        int Quantity,
        float Price);

}
