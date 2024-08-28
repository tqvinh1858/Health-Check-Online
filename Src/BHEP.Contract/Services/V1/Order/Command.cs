using BHEP.Contract.Abstractions.Message;
using static BHEP.Contract.Services.V1.Order.Responses;


namespace BHEP.Contract.Services.V1.Order;
public static class Command
{
   public record CreateOrderCommand(
       int UserId,
       int PaymentId,
       int? CodeId,
       float TotalPrice,
       List<OrderServiceDetailDto> OrderServiceDetails,
       List<OrderProductDetailDto> OrderProductDetails
       ) : ICommand<Responses.OrderResponse>;

    public record UpdateOrderCommand(
        int OrderId,
        int UserId,
        int PaymentId,
        int? CodeId,
        float TotalPrice,
        List<OrderServiceDetailDto> OrderServiceDetails,
        List<OrderProductDetailDto> OrderProductDetails
        ) : ICommand<Responses.OrderResponse>;

    public record DeleteOrderCommand(int Id) : ICommand;
}
