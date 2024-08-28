using System.ComponentModel.DataAnnotations;
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.PostLike;
public static class Command
{
    public record CreatePostLikeCommand(
        int UserId,
        int PostId) : ICommand<Responses.PostLikeResponse>;

    public record DeletePostLikeCommand(int Id) : ICommand;
}
