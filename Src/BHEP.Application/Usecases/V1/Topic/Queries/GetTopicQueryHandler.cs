using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Topic;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Topic.Queries;
public sealed class GetTopicQueryHandler : IQueryHandler<Query.GetTopicQuery, PagedResult<Responses.TopicResponse>>
{
    private readonly ITopicRepository TopicRepository;
    private readonly IMapper mapper;

    public GetTopicQueryHandler(
        ITopicRepository TopicRepository,
        IMapper mapper)
    {
        this.TopicRepository = TopicRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.TopicResponse>>> Handle(Query.GetTopicQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Check value search is nullOrWhiteSpace?
            var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? TopicRepository.FindAll()   // If Null GetAll
            : TopicRepository.FindAll(x => x.Name.Contains(request.searchTerm)); // If Not GetAll With Name Or Address Contain searchTerm

            // Get Func<TEntity,TResponse> column
            var keySelector = GetSortProperty(request);

            // Asc Or Des
            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.BlogEntities.Topic>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            var result = mapper.Map<PagedResult<Responses.TopicResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.BlogEntities.Topic, object>> GetSortProperty(Query.GetTopicQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "name" => e => e.Name,
        _ => e => e.Name
    };
}
