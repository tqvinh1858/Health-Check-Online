using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.ProductRate;
public static class Query
{
    public record GetProductRate(
        int ProductId,
        int PageIndex = 1,
        int PageSize = 10);
    public record GetProductRateQuery(
    int ProductId,
    string? searchTerm,
    string? sortColumn,
    SortOrder? sortOrder,
    int pageIndex,
    int pageSize) : IQuery<PagedResult<Responses.ProductRateResponse>>;
}
