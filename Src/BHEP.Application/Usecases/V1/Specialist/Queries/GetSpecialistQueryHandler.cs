using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Specialist;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Specialist.Queries;
public sealed class GetSpecialistQueryHandler : IQueryHandler<Query.GetSpecialistQuery, PagedResult<Responses.SpecialistResponse>>
{
    private readonly ISpecialistRepository SpecialistRepository;
    private readonly IMapper mapper;

    public GetSpecialistQueryHandler(ISpecialistRepository SpecialistRepository, IMapper mapper)
    {
        this.SpecialistRepository = SpecialistRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.SpecialistResponse>>> Handle(Query.GetSpecialistQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Check value search is nullOrWhiteSpace?
            var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? SpecialistRepository.FindAll()   // If Null GetAll
            : SpecialistRepository.FindAll(x => x.Name.Contains(request.searchTerm)); // If Not GetAll With Name Or Address Contain searchTerm

            // Get Func<TEntity,TResponse> column
            var keySelector = GetSortProperty(request);

            // Asc Or Des
            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.AppointmentEntities.Specialist>.CreateAsync(EventsQuery,
                request.pageIndex.Value,
                request.pageSize.Value);

            var result = mapper.Map<PagedResult<Responses.SpecialistResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.AppointmentEntities.Specialist, object>> GetSortProperty(Query.GetSpecialistQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "name" => e => e.Name,
        _ => e => e.Name
    };
}
