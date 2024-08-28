using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;

namespace BHEP.Contract.Services.V1.Reply;
public static class Query
{
    public record GetReplyQuery(
    int CommentId,
    int PageIndex,
    int PageSize) : IQuery<PagedResult<Responses.ReplyGetAllResponse>>;
}
