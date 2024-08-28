using System.ComponentModel.DataAnnotations;
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.CommentLike;
public static class Command
{
    public record CreateCommentLikeCommand(
        int UserId,
        int CommentId) : ICommand<Responses.CommentLikeResponse>;

    public record DeleteCommentLikeCommand(int Id) : ICommand;
}
