using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Topic;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.Topic.Commands;
public sealed class CreateTopicCommandHandler : ICommandHandler<Command.CreateTopicCommand, Responses.TopicResponse>
{
    private readonly ITopicRepository TopicRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateTopicCommandHandler(
        ITopicRepository TopicRepository,
        IMapper mapper,
        ApplicationDbContext context)
    {
        this.TopicRepository = TopicRepository;
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<Result<Responses.TopicResponse>> Handle(Command.CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var TopicNameExist = TopicRepository.FindAll(x => x.Name == request.Name)
            ?? throw new TopicException.TopicBadRequestException("TopicName is already exist");

        var Topic = new Domain.Entities.BlogEntities.Topic
        {
            Name = request.Name,
            Description = request.Description,
        };

        try
        {
            TopicRepository.Add(Topic);
            await context.SaveChangesAsync();

            var resultResponse = mapper.Map<Responses.TopicResponse>(Topic);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
