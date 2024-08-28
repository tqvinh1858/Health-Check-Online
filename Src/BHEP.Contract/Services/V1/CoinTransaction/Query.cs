using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.CoinTransaction;
public class Query
{
    public record GetCoinTransactionQuery(
        CoinTransactionType? Type,
        int PageIndex,
        int PageSize) : IQuery<PagedResult<Responses.CoinTransactionResponse>>;
    public record GetCoinTransactionByUserIdQuery(
        int UserId,
        int PageIndex,
        int PageSize) : IQuery<PagedResult<Responses.CoinTransactionResponse>>;

    public record GetCoinTransactionByIdQuery(int Id) : IQuery<Responses.CoinTransactionResponse>;
}
