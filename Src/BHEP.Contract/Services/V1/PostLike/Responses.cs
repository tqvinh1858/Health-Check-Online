using static BHEP.Contract.Services.V1.Post.Responses;
using static BHEP.Contract.Services.V1.ServiceRate.Responses;

namespace BHEP.Contract.Services.V1.PostLike;
public static class Responses
{
    public record PostLikeResponse(
        int Id,
        int UserId,
        int PostId);
}
