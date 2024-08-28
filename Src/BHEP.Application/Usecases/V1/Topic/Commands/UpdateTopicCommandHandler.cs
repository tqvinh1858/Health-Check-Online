using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Topic;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Topic.Commands;
public sealed class UpdateTopicCommandHandler : ICommandHandler<Command.UpdateTopicCommand, Responses.TopicResponse>
{
    private readonly ITopicRepository TopicRepository;
    private readonly IMapper mapper;

    public UpdateTopicCommandHandler(ITopicRepository TopicRepository, IMapper mapper)
    {
        this.TopicRepository = TopicRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.TopicResponse>> Handle(Command.UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var Topic = await TopicRepository.FindByIdAsync(request.Id.Value, cancellationToken)
            ?? throw new TopicException.TopicIdNotFoundException();

        try
        {
            Topic.Update(request.Name, request.Description);
            TopicRepository.Update(Topic);

            var resultResponse = mapper.Map<Responses.TopicResponse>(Topic);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
