using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Blog;
public static class Query
{
    public record GetBlog(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageIndex,
        int pageSize);
    public record GetBlogQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.BlogGetAllResponse>>;
    public record GetBlogByIdQuery(int Id) : IQuery<Responses.BlogGetByIdResponse>;
}
