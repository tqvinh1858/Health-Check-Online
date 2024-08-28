using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Role;
public static class Query
{
    public record GetRoleQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.RoleResponse>>;
    public record GetRoleByIdQuery(int Id) : IQuery<Responses.RoleResponse>;
}
