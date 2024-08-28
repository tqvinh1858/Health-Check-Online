using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Comment;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Comment.Queries;
public sealed class GetCommentQueryHandler : IQueryHandler<Query.GetCommentQuery, PagedResult<Responses.CommentGetAllResponse>>
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly ICommentLikeRepository commentLikeRepository;
    private readonly IMapper mapper;
    public GetCommentQueryHandler(
        ICommentRepository commentRepository,
        ICommentLikeRepository commentLikeRepository,
        IPostRepository postRepository,
        IMapper mapper)
    {
        this.commentRepository = commentRepository;
        this.commentLikeRepository = commentLikeRepository;
        this.postRepository = postRepository;
        this.mapper = mapper;
    }


    public async Task<Result<PagedResult<Responses.CommentGetAllResponse>>> Handle(Query.GetCommentQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.FindSingleAsync(
            predicate: x => x.Id == request.PostId && x.IsDeleted == false);
        if (post == null)
            return Result.Failure<PagedResult<Responses.CommentGetAllResponse>>("Post not found");

        try
        {

            var EventsQuery = commentRepository.FindAll(x => x.PostId == request.PostId && x.IsDeleted == false);

            EventsQuery = EventsQuery.OrderBy(x => x.CreatedDate);

            var listResponse = EventsQuery.Select(x => new Responses.CommentGetAllResponse(
                x.Id,
                x.UserId,
                x.PostId,
                x.Content,
                x.CreatedDate.ToString("dd-MM-yyyy"),
                mapper.Map<Contract.Services.V1.ServiceRate.Responses.UserResponse>(x.User),
                x.CommentLikes.Count(),
                x.Replies.Where(x => x.IsDeleted == false).Count())).ToList();

            var result = PagedResult<Responses.CommentGetAllResponse>.Create(
               listResponse,
               request.PageIndex,
               request.PageSize,
               EventsQuery.Count());


            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
