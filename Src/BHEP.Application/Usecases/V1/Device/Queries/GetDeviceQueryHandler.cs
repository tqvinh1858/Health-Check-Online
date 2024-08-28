using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Device;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Device.Queries;
public sealed class GetDeviceQueryHandler : IQueryHandler<Query.GetDeviceQuery, PagedResult<Responses.DeviceResponse>>
{
    private readonly IDeviceRepository deviceRepository;
    private readonly IMapper mapper;

    public GetDeviceQueryHandler(
        IDeviceRepository deviceRepository,
        IMapper mapper)
    {
        this.deviceRepository = deviceRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.DeviceResponse>>> Handle(Query.GetDeviceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Domain.Entities.SaleEntities.Device> EventsQuery;
            EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
               ? deviceRepository.FindAll()
               : deviceRepository.FindAll(x => x.Code.Contains(request.searchTerm));

            var keySelector = GetSortProperty(request);

            EventsQuery = request.sortOrder == SortOrder.Descending
              ? EventsQuery.OrderByDescending(keySelector)
              : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.SaleEntities.Device>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            List<Responses.DeviceResponse> DeviceGetAllResponses = new List<Responses.DeviceResponse>();
            foreach (var item in Events.items)
            {
                var DeviceGetAllResponse = new Responses.DeviceResponse(
                    item.Id,
                    item.ProductId,
                    item.TransactionId,
                    item.Code,
                    item.IsSale,
                    item.CreatedDate,
                    item.SaleDate
                );

                DeviceGetAllResponses.Add(DeviceGetAllResponse);
            }

            var result = PagedResult<Responses.DeviceResponse>.Create(
                  DeviceGetAllResponses,
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

    private static Expression<Func<Domain.Entities.SaleEntities.Device, object>> GetSortProperty(Query.GetDeviceQuery request)
      => request.sortColumn?.ToLower() switch
      {
          "code" => e => e.Code,
          "product" => e => e.ProductId,
          _ => e => e.Code
      };
}
