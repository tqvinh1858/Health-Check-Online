using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Topic;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Topic.Queries;
public sealed class GetTopicByIdQueryHandler : IQueryHandler<Query.GetTopicByIdQuery, Responses.TopicResponse>
{
    private readonly ITopicRepository TopicRepository;
    private readonly IMapper mapper;

    public GetTopicByIdQueryHandler(
        ITopicRepository TopicRepository,
        IMapper mapper)
    {
        this.TopicRepository = TopicRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.TopicResponse>> Handle(Query.GetTopicByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await TopicRepository.FindByIdAsync(request.Id)
            ?? throw new TopicException.TopicIdNotFoundException();

        var resultResponse = mapper.Map<Responses.TopicResponse>(result);
        return Result.Success(resultResponse);
    }
}
