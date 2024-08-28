using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V3.CoinTransaction;
public static class Command
{
    public record CreateCoinTransactionCommand(
        int UserId,
        decimal Amount,
        bool IsMinus,
        string Title,
        string? Description,
        bool IsGenerateCode,
        string? Code,
        List<int>? Vouchers,
        int? serviceId,
        List<ProductTransaction>? Products) : ICommand;

    public record ProductTransaction(
        int Id,
        int Quantity);
}
