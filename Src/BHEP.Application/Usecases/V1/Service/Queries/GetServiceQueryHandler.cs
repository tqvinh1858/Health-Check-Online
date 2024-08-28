using System.Linq.Expressions;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Service;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Service.Queries;
public sealed class GetServiceQueryHandler : IQueryHandler<Query.GetServiceQuery, PagedResult<Responses.ServiceGetAllResponse>>
{
    private readonly IServiceRepository serviceRepository;
    private readonly IServiceRateRepository serviceRateRepository;

    public GetServiceQueryHandler(
        IServiceRepository serviceRepository,
        IServiceRateRepository serviceRateRepository)
    {
        this.serviceRepository = serviceRepository;
        this.serviceRateRepository = serviceRateRepository;
    }

    public async Task<Result<PagedResult<Responses.ServiceGetAllResponse>>> Handle(Query.GetServiceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Domain.Entities.SaleEntities.Service> EventsQuery;

            if (!string.IsNullOrWhiteSpace(request.searchTerm) && request.serviceType.HasValue)
            {
                EventsQuery = serviceRepository.FindAll(x => x.Name.Contains(request.searchTerm) && x.Type == request.serviceType.Value);
            }
            else if (!string.IsNullOrWhiteSpace(request.searchTerm))
            {
                EventsQuery = serviceRepository.FindAll(x => x.Name.Contains(request.searchTerm));
            }
            else if (request.serviceType.HasValue)
            {
                EventsQuery = serviceRepository.FindAll(x => x.Type == request.serviceType.Value);
            }
            else
            {
                EventsQuery = serviceRepository.FindAll();
            }

            var keySelector = GetSortProperty(request);

            EventsQuery = request.sortOrder == SortOrder.Descending
              ? EventsQuery.OrderByDescending(keySelector)
              : EventsQuery.OrderBy(keySelector);


            var Events = await PagedResult<Domain.Entities.SaleEntities.Service>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            List<Responses.ServiceGetAllResponse> ServiceRateList = new List<Responses.ServiceGetAllResponse>();

            foreach (var item in Events.items)
            {
                var serviceGetAllResponse = new Responses.ServiceGetAllResponse(
                    item.Id,
                    item.Image,
                    item.Name,
                    item.Type,
                    item.Description,
                    item.Price,
                    item.Duration,
                    await serviceRateRepository.GetAvgRating(item.Id)
                    );
                ServiceRateList.Add(serviceGetAllResponse);
            }

            var result = PagedResult<Responses.ServiceGetAllResponse>.Create(
                ServiceRateList,
                Events.pageIndex,
                Events.pageSize,
                EventsQuery.Count()
            );

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public static Expression<Func<Domain.Entities.SaleEntities.Service, object>> GetSortProperty(Query.GetServiceQuery request)
        => request.sortColumn?.ToLower() switch
        {
            "name" => e => e.Name,
            "type" => e => e.Type,
            _ => e => e.Name
        };
}
