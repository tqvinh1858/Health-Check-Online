using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Comment;
public class Command
{
    public record CreateCommentCommand(
          int UserId,
          int PostId,
          string Content) : ICommand<Responses.CommentResponse>;

    public record UpdateCommentCommand(
            int? Id,
            string Content) : ICommand<Responses.CommentResponse>;

    public record DeleteCommentCommand(int Id) : ICommand;
}
