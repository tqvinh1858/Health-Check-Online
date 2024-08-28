using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Blog;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Blog.Queries;
public sealed class GetBlogQueryHandler : IQueryHandler<Query.GetBlogQuery, PagedResult<Responses.BlogGetAllResponse>>
{
    private readonly IBlogRepository BlogRepository;
    private readonly IMapper mapper;

    public GetBlogQueryHandler(
        IBlogRepository BlogRepository,
        IMapper mapper)
    {
        this.BlogRepository = BlogRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.BlogGetAllResponse>>> Handle(Query.GetBlogQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Expression<Func<Domain.Entities.BlogEntities.Blog, object>>[] include = [x => x.User, x => x.BlogPhotos];
            // Check value search is nullOrWhiteSpace?
            var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? BlogRepository.FindAll(x => x.IsDeleted == false, includeProperties: include)   // If Null GetAll
            : BlogRepository.FindAll(x => x.Title.Contains(request.searchTerm) && x.IsDeleted == false, includeProperties: include);

            // Get Func<TEntity,TResponse> column
            var keySelector = GetSortProperty(request);

            // Asc Or Des
            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.BlogEntities.Blog>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            // Convert To BlogGetAllResponse
            List<Responses.BlogGetAllResponse> listResponse = new List<Responses.BlogGetAllResponse>();
            foreach (var item in Events.items)
            {
                var response = new Responses.BlogGetAllResponse(
                    item.Id,
                    item.UserId,
                    item.User.FullName,
                    item.Title,
                    item.Content,
                    item.View,
                    item.BlogPhotos.FirstOrDefault().Image
                    );
                listResponse.Add(response);
            }

            var result = PagedResult<Responses.BlogGetAllResponse>.Create(
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

    public static Expression<Func<Domain.Entities.BlogEntities.Blog, object>> GetSortProperty(Query.GetBlogQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "title" => e => e.Title,
        "createddate" => e => e.CreatedDate,
        _ => e => e.UpdatedDate
    };
}
