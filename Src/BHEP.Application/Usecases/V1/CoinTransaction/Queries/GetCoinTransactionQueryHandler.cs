using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.CoinTransaction;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.CoinTransaction.Queries;
public sealed class GetCoinTransactionQueryHandler : IQueryHandler<Query.GetCoinTransactionQuery, PagedResult<Responses.CoinTransactionResponse>>
{
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly IMapper mapper;

    public GetCoinTransactionQueryHandler(
        ICoinTransactionRepository coinTransactionRepository,
        IMapper mapper)
    {
        this.coinTransactionRepository = coinTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.CoinTransactionResponse>>> Handle(Query.GetCoinTransactionQuery request, CancellationToken cancellationToken)
    {
        var EventsQuery = request.Type is null
            ? coinTransactionRepository.FindAll()
            : coinTransactionRepository.FindAll(x => x.Type == request.Type);

        var Events = await PagedResult<Domain.Entities.SaleEntities.CoinTransaction>.CreateAsync(EventsQuery,
            request.PageIndex,
            request.PageSize);

        var listCoinTransactionResponses = new List<Responses.CoinTransactionResponse>();
        foreach (var transaction in Events.items)
        {
            var response = new Responses.CoinTransactionResponse
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Amount = transaction.Amount,
                IsMinus = transaction.IsMinus,
                Title = transaction.Title,
                Description = transaction.Description,
                FamilyCode = transaction.Code == null ? string.Empty : transaction.Code.Name,
                CreatedDate = transaction.CreatedDate.ToString("dd-MM-yyyy"),
                Vouchers = transaction.VoucherTransaction.Any()
                    ? mapper.Map<List<Responses.VoucherResponse>>(transaction.VoucherTransaction.Select(x => x.Voucher))
                    : null,
                Service = transaction.ServiceTransactions.Any()
                    ? mapper.Map<Responses.ServiceResponse>(transaction.ServiceTransactions.FirstOrDefault().Service)
                    : null,
                Products = transaction.ProductTransactions.Any()
                    ? mapper.Map<List<Responses.ProductResponse>>(transaction.ProductTransactions.Select(x => x.Product))
                    : null,
                Devices = transaction.Devices.Any()
                    ? mapper.Map<List<Responses.DeviceResponse>>(transaction.Devices)
                    : null,
            };
            listCoinTransactionResponses.Add(response);
        }

        var result = PagedResult<Responses.CoinTransactionResponse>.Create(
            listCoinTransactionResponses,
            request.PageIndex,
            request.PageSize,
            EventsQuery.Count());

        return Result.Success(result);

    }
}
