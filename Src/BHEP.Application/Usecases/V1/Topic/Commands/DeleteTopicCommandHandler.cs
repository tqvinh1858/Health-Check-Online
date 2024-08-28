using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Topic;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Topic.Commands;
public sealed class DeleteTopicCommandHandler : ICommandHandler<Command.DeleteTopicCommand>
{
    private readonly ITopicRepository TopicRepository;
    public DeleteTopicCommandHandler(ITopicRepository TopicRepository)
    {
        this.TopicRepository = TopicRepository;
    }

    public async Task<Result> Handle(Command.DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        var TopicExist = await TopicRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new TopicException.TopicIdNotFoundException();

        try
        {
            TopicRepository.Remove(TopicExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
