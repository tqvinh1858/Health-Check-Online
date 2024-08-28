using static BHEP.Contract.Services.V1.ServiceRate.Responses;

namespace BHEP.Contract.Services.V1.Comment;
public static class Responses
{
    public record CommentGetAllResponse(
       int Id,
       int UserId,
       int PostId,
       string Content,
       string Time,
       UserResponse User,
       int TotalLike,
       int TotalReply
       );

    public record CommentResponse(
       int Id,
       int UserId,
       int PostId,
       string Content
       );
}
