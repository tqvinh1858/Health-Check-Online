using static BHEP.Contract.Services.V1.ServiceRate.Responses;

namespace BHEP.Contract.Services.V1.Reply;
public static class Responses
{
    public record ReplyGetAllResponse(
       int Id,
       int UserId,
       int CommentId,
       int? ReplyParentId,
       string Content,
       string Time,
       UserResponse User,
       UserResponse? UserReplyParent,
       int TotalLike
       );

    public record ReplyResponse(
       int Id,
       int UserId,
       int CommentId,
       int? ReplyParentId,
       string Content
       );
}
