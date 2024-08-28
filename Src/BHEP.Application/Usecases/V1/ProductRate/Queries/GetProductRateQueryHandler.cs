using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.ProductRate;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.ProductRate.Queries;
public sealed class GetProductRateQueryHandler : IQueryHandler<Query.GetProductRateQuery, PagedResult<Responses.ProductRateResponse>>
{
    private readonly IProductRateRepository ProductRateRepository;
    private readonly IMapper mapper;
    public GetProductRateQueryHandler(
        IProductRateRepository ProductRateRepository,
        IMapper mapper)
    {
        this.ProductRateRepository = ProductRateRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.ProductRateResponse>>> Handle(Query.GetProductRateQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.SaleEntities.ProductRate> EventsQuery;

        EventsQuery = ProductRateRepository.FindAll(x => x.ProductId == request.ProductId && x.IsDeleted == false);

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.SaleEntities.ProductRate>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        List<Responses.ProductRateResponse> listResponses = new List<Responses.ProductRateResponse>();

        foreach (var item in Events.items)
        {
            var productRateResponse = new Responses.ProductRateResponse(
                item.Id,
                item.TransactionId,
                item.Reason,
                item.Rate,
                mapper.Map<Responses.UserResponse>(item.User));

            listResponses.Add(productRateResponse);
        }

        var result = PagedResult<Responses.ProductRateResponse>.Create(
            listResponses,
            Events.pageIndex,
            Events.pageSize,
            EventsQuery.Count());

        return Result.Success(result);
    }

    private static Expression<Func<Domain.Entities.SaleEntities.ProductRate, object>> GetSortProperty(Query.GetProductRateQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "updatedDate" => e => e.UpdatedDate,
        "createddate" => e => e.CreatedDate,
        _ => e => e.UpdatedDate
    };
}
