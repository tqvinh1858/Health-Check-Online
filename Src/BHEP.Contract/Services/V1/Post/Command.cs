using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Post;
public class Command
{
    public record CreatePostCommand(
          int UserId,
          List<int> Specialists,
          string Title,
          string Content,
          int Age,
          Gender Gender) : ICommand<Responses.PostResponse>;

    public record UpdatePostCommand(
            int? Id,
            List<int> Specialists,
            string Title,
            string Content,
            int Age,
            Gender Gender,
            PostStatus Status) : ICommand<Responses.PostResponse>;

    public record DeletePostCommand(int Id) : ICommand;
}
