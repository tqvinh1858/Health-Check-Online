using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.ServiceRate;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.ServiceRate.Queries;
public sealed class GetServiceRateQueryHandler : IQueryHandler<Query.GetServiceRateQuery, PagedResult<Responses.ServiceRateResponse>>
{
    private readonly IServiceRateRepository ServiceRateRepository;
    private readonly IMapper mapper;
    public GetServiceRateQueryHandler(
        IServiceRateRepository ServiceRateRepository,
        IMapper mapper)
    {
        this.ServiceRateRepository = ServiceRateRepository;
        this.mapper = mapper;
    }
    public async Task<Result<PagedResult<Responses.ServiceRateResponse>>> Handle(Query.GetServiceRateQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.SaleEntities.ServiceRate> EventsQuery;

        EventsQuery = ServiceRateRepository.FindAll(x => x.ServiceId == request.ServiceId && x.IsDeleted == false);

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.SaleEntities.ServiceRate>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        List<Responses.ServiceRateResponse> listResponses = new List<Responses.ServiceRateResponse>();

        foreach (var item in Events.items)
        {
            var productRateResponse = new Responses.ServiceRateResponse(
                item.Id,
                item.TransactionId,
                item.Reason,
                item.Rate,
                mapper.Map<Responses.UserResponse>(item.User));

            listResponses.Add(productRateResponse);
        }

        var result = PagedResult<Responses.ServiceRateResponse>.Create(
            listResponses,
            Events.pageIndex,
            Events.pageSize,
            EventsQuery.Count());

        return Result.Success(result);
    }

    private static Expression<Func<Domain.Entities.SaleEntities.ServiceRate, object>> GetSortProperty(Query.GetServiceRateQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "updatedDate" => e => e.UpdatedDate,
        "createddate" => e => e.CreatedDate,
        _ => e => e.UpdatedDate
    };
}
