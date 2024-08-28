using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Product;
public static class Query
{
    public record GetProduct(
       string? SearchTerm = null,
       string? SortColumn = null,
       string? SortOrder = null,
       int PageIndex = 1,
       int PageSize = 10
       );


    public record GetProductQuery(
       string? searchTerm,
       string? sortColumn,
       SortOrder? sortOrder,
       int pageIndex,
       int pageSize
      ) : IQuery<PagedResult<Responses.ProductGetAllResponse>>;


    public record GetProductByIdQuery(int Id) : IQuery<Responses.ProductGetByIdResponse>;

}
