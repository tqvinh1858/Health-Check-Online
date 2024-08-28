using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.JobApplication;
public static class Query
{
    public record GetJobApplication(
        string? SearchTerm = null,
        ApplicationStatus? Status = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10);

    public record GetJobApplicationQuery(
        string? searchTerm,
        ApplicationStatus? status,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.JobApplicationResponse>>;
    public record GetJobApplicationByIdQuery(int Id) : IQuery<Responses.JobApplicationGetByIdResponse>;
}
