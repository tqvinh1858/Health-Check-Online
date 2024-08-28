
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Topic;
public class Command
{
    public record CreateTopicCommand(
        string Name,
        string Description) : ICommand<Responses.TopicResponse>;

    public record UpdateTopicCommand(
        int? Id,
        string Name,
        string Description) : ICommand<Responses.TopicResponse>;

    public record DeleteTopicCommand(int Id) : ICommand;
}
