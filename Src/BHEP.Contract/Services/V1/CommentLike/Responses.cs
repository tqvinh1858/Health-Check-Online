using static BHEP.Contract.Services.V1.Comment.Responses;
using static BHEP.Contract.Services.V1.ServiceRate.Responses;

namespace BHEP.Contract.Services.V1.CommentLike;
public static class Responses
{
    public record CommentLikeResponse(
        int Id,
        int UserId,
        int CommentId);
}
