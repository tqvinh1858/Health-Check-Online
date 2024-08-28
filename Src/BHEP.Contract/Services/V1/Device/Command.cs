using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Device;
public class Command
{
    public record CreateDeviceCommand(int Quantity) : ICommand;

    public record UpdateDeviceCommand(
            int Id,
            int ProductId,
            int? TransactionId,
            string Code,
            bool IsSale,
            DateTime? SaleDate
        ) : ICommand<Responses.DeviceResponse>;

    public record DeleteDeviceCommand(int Id) : ICommand;
}
