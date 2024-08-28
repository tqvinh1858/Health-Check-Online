using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;

namespace BHEP.Contract.Services.V1.ProductService;
public static class Query
{
    public record GetOutstandingQuery(int Number) : IQuery<PagedResult<Responses.OutstandingResponse>>;

    public record GetProductService(
        int PageIndex = 1,
        int PageSize = 10) : IQuery<PagedResult<Responses.ProductServiceResponse>>;
}
