using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.DeletionRequest;
public static class Query
{
    public record GetDeletionRequest(
       string? SearchTerm = null,
       DeletionRequestStatus? Status = null,
       string? SortColumn = null,
       string? SortOrder = null,
       int PageIndex = 1,
       int PageSize = 10);

    public record GetDeletionRequestQuery(
       string? searchTerm,
       DeletionRequestStatus? status,
       string? sortColumn,
       SortOrder? sortOrder,
       int pageIndex,
       int pageSize) : IQuery<PagedResult<Responses.DeletionRequestResponse>>;

    public record GetDeletionRequestByIdQuery(int Id) : IQuery<Responses.DeletionRequestGetByIdResponse>;

    public record GetDeletionRequestByUserIdQuery(int UserId) : IQuery<Responses.DeletionRequestResponse>;

}
