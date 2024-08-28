using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Order;
public static class Query
{
    public record GetOrder(
        string? SearchTerm = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10
        );

    public record GetOrderQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder sortOrder,
        int pageIndex,
        int pageSize
        ) : IQuery<PagedResult<Responses.OrderResponse>>;
         
    public record GetOrderByIdQuery(int Id) : IQuery<Responses.OrderResponse>;
}
