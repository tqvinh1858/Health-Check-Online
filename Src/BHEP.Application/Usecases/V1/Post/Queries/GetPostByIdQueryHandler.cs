using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Post;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Post.Queries;
public sealed class GetPostByIdQueryHandler : IQueryHandler<Query.GetPostByIdQuery, Responses.PostGetByIdResponse>
{
    private readonly IPostRepository postRepository;
    private readonly IPostLikeRepository postLikeRepository;
    private readonly IMapper mapper;

    public GetPostByIdQueryHandler(
        IPostRepository postRepository,
        IMapper mapper,
        IPostLikeRepository postLikeRepository)
    {
        this.postRepository = postRepository;
        this.mapper = mapper;
        this.postLikeRepository = postLikeRepository;
    }

    public async Task<Result<Responses.PostGetByIdResponse>> Handle(Query.GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.FindSingleAsync(predicate: x => x.Id == request.Id && x.IsDeleted == false);
        if (post == null)
            return Result.Failure<Responses.PostGetByIdResponse>("Post not found");



        var comments = await postRepository.GetComments(post.Id);
        var CommentsResponse = mapper.Map<List<Responses.CommentResponse>>(comments);

        var Specialist = await postRepository.GetSpecialist(post.Id);
        var SpecialistsResponse = mapper.Map<List<Contract.Services.V1.Specialist.Responses.SpecialistResponse>>(Specialist);


        int totalLike = await postLikeRepository.GetTotalLike(request.Id);
        var resultResponse = new Responses.PostGetByIdResponse(
            post.Id,
            post.UserId,
            post.Title,
            post.Content,
            post.Age,
            post.Gender,
            post.Status,
            CommentsResponse,
            SpecialistsResponse,
            totalLike
            );

        return Result.Success(resultResponse);
    }
}
