using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.UserRate;
public static class Query
{
    public record GetUser(
        int EmployeeId,
        int PageIndex = 1,
        int PageSize = 10);
    public record GetUserRateQuery(
    int EmployeeId,
    string? searchTerm,
    string? sortColumn,
    SortOrder? sortOrder,
    int pageIndex,
    int pageSize) : IQuery<PagedResult<Responses.UserRateResponse>>;
}
