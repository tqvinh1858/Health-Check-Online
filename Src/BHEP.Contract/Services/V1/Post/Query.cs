using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Post;
public static class Query
{
    public record GetPost(
       string? searchTerm,
       string? sortColumn,
       string? sortOrder,
       int pageIndex,
       int pageSize);


    public record GetPostQuery(
      string? searchTerm,
      string? sortColumn,
      SortOrder? sortOrder,
      int pageIndex,
      int pageSize) : IQuery<PagedResult<Responses.PostGetAllResponse>>;

    public record GetPostByIdQuery(int Id) : IQuery<Responses.PostGetByIdResponse>;

}
