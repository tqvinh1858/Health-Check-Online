using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.CoinTransaction;
using BHEP.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.CoinTransaction.Queries;
public sealed class GetCoinTransactionByUserIdQueryHandler : IQueryHandler<Query.GetCoinTransactionByUserIdQuery, PagedResult<Responses.CoinTransactionResponse>>
{
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly IMapper mapper;
    public GetCoinTransactionByUserIdQueryHandler(
        ICoinTransactionRepository coinTransactionRepository,
        IMapper mapper)
    {
        this.coinTransactionRepository = coinTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.CoinTransactionResponse>>> Handle(Query.GetCoinTransactionByUserIdQuery request, CancellationToken cancellationToken)
    {
        // get ListTransaction By UserId
        var listTransaction = await coinTransactionRepository.FindAll(predicate: x => x.UserId == request.UserId && x.IsDeleted == false).ToListAsync(cancellationToken);

        List<Responses.CoinTransactionResponse> listResponse = new List<Responses.CoinTransactionResponse>();
        // Convert to Response
        foreach (var transaction in listTransaction)
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
            listResponse.Add(response);
        }


        // Create PageResult
        var result = PagedResult<Responses.CoinTransactionResponse>.Create(
                listResponse,
                request.PageIndex,
                request.PageSize,
                listTransaction.Count());

        return Result.Success(result);
    }
}
