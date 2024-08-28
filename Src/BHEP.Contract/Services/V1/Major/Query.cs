using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Major;
public static class Query 
{
    public record GetMajorQuery(
       string? searchTerm,
       string? sortColumn,
       SortOrder? sortOrder,
       int pageIndex,
       int pageSize) : IQuery<PagedResult<Responses.MajorResponse>>;

    public record GetMajorByIdQuery(int Id) : IQuery<Responses.MajorResponse>;
}
