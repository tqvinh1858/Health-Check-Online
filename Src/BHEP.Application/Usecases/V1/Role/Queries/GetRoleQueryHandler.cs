using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Role;
using BHEP.Domain.Abstractions.Repositories;


namespace BHEP.Application.Usecases.V1.Role.Queries;
public sealed class GetRoleQueryHandler : IQueryHandler<Query.GetRoleQuery, PagedResult<Responses.RoleResponse>>
{
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;

    public GetRoleQueryHandler(
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.RoleResponse>>> Handle(Query.GetRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Check value search is nullOrWhiteSpace?
            var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? roleRepository.FindAll()   // If Null GetAll
            : roleRepository.FindAll(x => x.Name.Contains(request.searchTerm)); // If Not GetAll With Name Or Address Contain searchTerm

            // Get Func<TEntity,TResponse> column
            var keySelector = GetSortProperty(request);

            // Asc Or Des
            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.UserEntities.Role>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            var result = mapper.Map<PagedResult<Responses.RoleResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.UserEntities.Role, object>> GetSortProperty(Query.GetRoleQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "name" => e => e.Name,
        _ => e => e.Name
    };
}
