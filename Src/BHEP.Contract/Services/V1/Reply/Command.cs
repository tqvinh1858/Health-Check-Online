using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Reply;
public class Command
{
    public record CreateReplyCommand(
          int UserId,
          int CommentId,
          int? ReplyParentId,
          string Content) : ICommand<Responses.ReplyResponse>;

    public record UpdateReplyCommand(
            int? Id,
            string Content) : ICommand<Responses.ReplyResponse>;

    public record DeleteReplyCommand(int Id) : ICommand;
}
