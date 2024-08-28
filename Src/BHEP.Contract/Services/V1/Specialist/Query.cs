using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Specialist;
public static class Query
{
    public record GetSpecialist(
        string? SearchTerm = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10);
    public record GetSpecialistQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int? pageIndex,
        int? pageSize)
        : IQuery<PagedResult<Responses.SpecialistResponse>>;

    public record GetSpecialistByIdQuery(int Id)
        : IQuery<Responses.SpecialistGetByIdResponse>;
}
