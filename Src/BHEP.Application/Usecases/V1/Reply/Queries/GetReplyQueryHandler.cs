using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Reply;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Reply.Queries;
public sealed class GetReplyQueryHandler : IQueryHandler<Query.GetReplyQuery, PagedResult<Responses.ReplyGetAllResponse>>
{
    private readonly ICommentRepository CommentRepository;
    private readonly IReplyRepository ReplyRepository;
    private readonly IMapper mapper;
    public GetReplyQueryHandler(
        IReplyRepository ReplyRepository,
        ICommentRepository CommentRepository,
        IMapper mapper)
    {
        this.ReplyRepository = ReplyRepository;
        this.CommentRepository = CommentRepository;
        this.mapper = mapper;
    }


    public async Task<Result<PagedResult<Responses.ReplyGetAllResponse>>> Handle(Query.GetReplyQuery request, CancellationToken cancellationToken)
    {
        var comment = await CommentRepository.FindSingleAsync(
            predicate: x => x.Id == request.CommentId && x.IsDeleted == false);
        if (comment == null)
            return Result.Failure<PagedResult<Responses.ReplyGetAllResponse>>("Comment not found");

        try
        {

            var EventsQuery = ReplyRepository.FindAll(x => x.CommentId == request.CommentId && x.IsDeleted == false);

            EventsQuery = EventsQuery.OrderBy(x => x.CreatedDate);

            var listResponse = EventsQuery.Select(x => new Responses.ReplyGetAllResponse(
                x.Id,
                x.UserId,
                x.CommentId,
                x.ReplyParentId,
                x.Content,
                x.CreatedDate.ToString("dd-MM-yyyy"),
                mapper.Map<Contract.Services.V1.ServiceRate.Responses.UserResponse>(x.User),
                mapper.Map<Contract.Services.V1.ServiceRate.Responses.UserResponse>(x.ReplyParent.User),
                x.ReplyLikes.Count())).ToList();

            var result = PagedResult<Responses.ReplyGetAllResponse>.Create(
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
