using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Voucher;
public class Command
{
    public record CreateVoucherCommand(
            string Name,
            string Code,
            float Discount,
            int Stock,
            DateTime StartDate,
            DateTime EndDate) : ICommand<Responses.VoucherResponse>;

    public record UpdateVoucherCommand(
        int Id,
        string Name,
        string Code,
        float Discount,
        int Stock,
        DateTime StartDate,
        DateTime EndDate
        ) : ICommand<Responses.VoucherResponse>;

    public record DeleteVoucherCommand(int Id) : ICommand;
}
