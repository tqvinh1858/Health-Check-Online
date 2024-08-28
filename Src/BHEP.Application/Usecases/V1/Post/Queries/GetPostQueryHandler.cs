using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Post;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Post.Queries;
public sealed class GetPostQueryHandler : IQueryHandler<Query.GetPostQuery, PagedResult<Responses.PostGetAllResponse>>
{
    private readonly IPostRepository postRepository;
    private readonly IPostLikeRepository postLikeRepository;
    private readonly IMapper mapper;

    public GetPostQueryHandler(
        IPostRepository postRepository,
        IMapper mapper,
        IPostLikeRepository postLikeRepository)
    {
        this.postRepository = postRepository;
        this.mapper = mapper;
        this.postLikeRepository = postLikeRepository;
    }

    public async Task<Result<PagedResult<Responses.PostGetAllResponse>>> Handle(Query.GetPostQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
                ? postRepository.FindAll(x => x.IsDeleted == false && x.Status == PostStatus.Published)
                : postRepository.FindAll(x => x.Title.Contains(request.searchTerm) && x.IsDeleted == false && x.Status == PostStatus.Published);

            var keySelector = GetSortProperty(request);

            EventsQuery = request.sortOrder == SortOrder.Descending
                 ? EventsQuery.OrderByDescending(keySelector)
                 : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.PostEntities.Post>.CreateAsync(EventsQuery,
               request.pageIndex,
               request.pageSize);

            List<Responses.PostGetAllResponse> listResponse = new List<Responses.PostGetAllResponse>();


            foreach (var item in Events.items)
            {
                int TotalLike = await postLikeRepository.GetTotalLike(item.Id);
                int TotalCommentAndReply = await postRepository.GetTotalCommentAndReply(item.Id);

                var Specialist = await postRepository.GetSpecialist(item.Id);
                var SpecialistsResponse = mapper.Map<List<Contract.Services.V1.Specialist.Responses.SpecialistResponse>>(Specialist);

                var response = new Responses.PostGetAllResponse(
                    item.Id,
                    item.UserId,
                    item.User.FullName,
                    item.Title,
                    item.Content,
                    item.Age,
                    item.Gender,
                    item.Status,
                    SpecialistsResponse,
                    TotalLike,
                    TotalCommentAndReply
                    );
                listResponse.Add(response);
            }

            var result = PagedResult<Responses.PostGetAllResponse>.Create(
               listResponse,
               Events.pageIndex,
               Events.pageSize,
               EventsQuery.Count());


            return Result.Success(result);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.PostEntities.Post, object>> GetSortProperty(Query.GetPostQuery request)
      => request.sortColumn?.ToLower() switch
      {
          "title" => e => e.Title,
          "age" => e => e.Age,
          _ => e => e.Title
      };
}
