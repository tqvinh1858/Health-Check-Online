using System.Linq.Expressions;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Voucher;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Voucher.Queries;
public sealed class GetVoucherQueryHandler : IQueryHandler<Query.GetVoucherQuery, PagedResult<Responses.VoucherResponse>>
{
    private readonly IVoucherRepository voucherRepository;

    public GetVoucherQueryHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }

    public async Task<Result<PagedResult<Responses.VoucherResponse>>> Handle(Query.GetVoucherQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Domain.Entities.SaleEntities.Voucher> EventsQuery;
            EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
               ? voucherRepository.FindAll()
               : voucherRepository.FindAll(x => x.Code.Contains(request.searchTerm));

            var keySelector = GetSortProperty(request);

            EventsQuery = request.sortOrder == SortOrder.Descending
              ? EventsQuery.OrderByDescending(keySelector)
              : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.SaleEntities.Voucher>.CreateAsync(EventsQuery,
                   request.pageIndex,
                   request.pageSize);

            List<Responses.VoucherResponse> VoucherGetAllResponses = new List<Responses.VoucherResponse>();
            foreach (var item in Events.items)
            {
                var DeviceGetAllResponse = new Responses.VoucherResponse(
                    item.Id,
                    item.Name,
                    item.Code,
                    item.Discount,
                    item.Stock,
                    item.IsExpired,
                    item.OutOfStock
                );

                VoucherGetAllResponses.Add(DeviceGetAllResponse);
            }


            var result = PagedResult<Responses.VoucherResponse>.Create(
                  VoucherGetAllResponses,
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


    private static Expression<Func<Domain.Entities.SaleEntities.Voucher, object>> GetSortProperty(Query.GetVoucherQuery request)
      => request.sortColumn?.ToLower() switch
      {
          "discount" => e => e.Discount,
          "stock" => e => e.Stock,
          _ => e => e.Discount
      };
}
