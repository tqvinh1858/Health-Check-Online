namespace BHEP.Contract.Services.V1.ReplyLike;
public static class Responses
{
    public record ReplyLikeResponse(
        int Id,
        int UserId,
        int ReplyId);
}
