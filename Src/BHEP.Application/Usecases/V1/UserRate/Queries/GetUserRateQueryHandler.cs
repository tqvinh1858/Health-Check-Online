using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.UserRate;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.UserRate.Queries;
public sealed class GetUserRateQueryHandler : IQueryHandler<Query.GetUserRateQuery, PagedResult<Responses.UserRateResponse>>
{
    private readonly IUserRateRepository UserRateRepository;
    private readonly IMapper mapper;
    public GetUserRateQueryHandler(
        IUserRateRepository UserRateRepository,
        IMapper mapper)
    {
        this.UserRateRepository = UserRateRepository;
        this.mapper = mapper;
    }
    public async Task<Result<PagedResult<Responses.UserRateResponse>>> Handle(Query.GetUserRateQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.UserEntities.UserRate> EventsQuery;

        EventsQuery = UserRateRepository.FindAll(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false);

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.UserEntities.UserRate>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = mapper.Map<PagedResult<Responses.UserRateResponse>>(Events);

        return Result.Success(result);
    }

    private static Expression<Func<Domain.Entities.UserEntities.UserRate, object>> GetSortProperty(Query.GetUserRateQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "updatedDate" => e => e.UpdatedDate,
        "createddate" => e => e.CreatedDate,
        _ => e => e.UpdatedDate
    };
}
