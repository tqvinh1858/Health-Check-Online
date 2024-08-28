using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Major;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Major.Queries;
public sealed class GetMajorQueryHandler : IQueryHandler<Query.GetMajorQuery, PagedResult<Responses.MajorResponse>>
{
    private readonly IMajorRepository majorRepository;
    private readonly IMapper mapper;

    public GetMajorQueryHandler(
        IMajorRepository majorRepository,
        IMapper mapper)
    {
        this.majorRepository = majorRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.MajorResponse>>> Handle(Query.GetMajorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? majorRepository.FindAll()
            : majorRepository.FindAll(x => x.Name.Contains(request.searchTerm));

            var keySelector = GetSortProperty(request);

            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.Major>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

            var result = mapper.Map<PagedResult<Responses.MajorResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public static Expression<Func<Domain.Entities.Major, object>> GetSortProperty(Query.GetMajorQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "name" => e => e.Name,
        _ => e => e.Name
    };
}
