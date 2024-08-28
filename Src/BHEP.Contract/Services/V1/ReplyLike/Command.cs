using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.ReplyLike;
public static class Command
{
    public record CreateReplyLikeCommand(
        int UserId,
        int ReplyId) : ICommand<Responses.ReplyLikeResponse>;

    public record DeleteReplyLikeCommand(int Id) : ICommand;
}
