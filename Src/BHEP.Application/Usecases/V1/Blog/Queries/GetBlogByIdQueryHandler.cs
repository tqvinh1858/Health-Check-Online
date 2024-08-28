using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Blog;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Blog.Queries;
public sealed class GetBlogByIdQueryHandler : IQueryHandler<Query.GetBlogByIdQuery, Responses.BlogGetByIdResponse>
{
    private readonly IBlogRepository BlogRepository;
    private readonly IMapper mapper;

    public GetBlogByIdQueryHandler(
        IBlogRepository BlogRepository,
        IMapper mapper)
    {
        this.BlogRepository = BlogRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.BlogGetByIdResponse>> Handle(Query.GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await BlogRepository.FindSingleAsync(
            predicate: x => x.IsDeleted == false && x.Id == request.Id,
            cancellationToken)
            ?? throw new BlogException.BlogIdNotFoundException();


        // getListTopic
        List<Responses.TopicResponse> listTopics = new List<Responses.TopicResponse>();
        foreach (var blogTopic in blog.BlogTopics.ToList())
        {
            var topicResponse = new Responses.TopicResponse(
                blogTopic.Topic.Id,
                blogTopic.Topic.Name);

            listTopics.Add(topicResponse);
        }

        //GetAvgRate
        float avgRate = blog.BlogRates.Any() ? blog.BlogRates.Average(x => x.Rate) : 0;

        var resultResponse = new Responses.BlogGetByIdResponse(
            blog.Id,
            blog.UserId,
            blog.User.FullName,
            blog.Title,
            blog.Content,
            blog.View,
            avgRate,
            listTopics,
            mapper.Map<ICollection<Responses.PhotoResponse>>(blog.BlogPhotos)
            );
        return Result.Success(resultResponse);
    }
}
