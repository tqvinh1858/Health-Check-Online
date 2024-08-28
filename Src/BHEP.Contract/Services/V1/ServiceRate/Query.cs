using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.ServiceRate;
public static class Query
{
    public record GetServiceRate(
        int ServiceId,
        int PageIndex = 1,
        int PageSize = 10);
    public record GetServiceRateQuery(
        int ServiceId,
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.ServiceRateResponse>>;
}
