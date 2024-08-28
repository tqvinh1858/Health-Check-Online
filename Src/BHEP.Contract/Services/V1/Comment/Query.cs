using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;

namespace BHEP.Contract.Services.V1.Comment;
public static class Query
{
    public record GetCommentQuery(
        int PostId,
        int PageIndex,
        int PageSize) : IQuery<PagedResult<Responses.CommentGetAllResponse>>;
}
