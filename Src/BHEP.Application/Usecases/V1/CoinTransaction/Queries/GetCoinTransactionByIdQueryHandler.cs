using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.CoinTransaction;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.CoinTransaction.Queries;
public sealed class GetCoinTransactionByIdQueryHandler : IQueryHandler<Query.GetCoinTransactionByIdQuery, Responses.CoinTransactionResponse>
{
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly IMapper mapper;

    public GetCoinTransactionByIdQueryHandler(
        ICoinTransactionRepository coinTransactionRepository,
        IMapper mapper)
    {
        this.coinTransactionRepository = coinTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.CoinTransactionResponse>> Handle(Query.GetCoinTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await coinTransactionRepository.FindSingleAsync(
            predicate: x => x.IsDeleted == false && x.Id == request.Id,
            cancellationToken);
        if (transaction == null)
            return Result.Failure<Responses.CoinTransactionResponse>("CoinTransaction not found.");

        var voucherResponses = transaction.VoucherTransaction.Any() ? mapper.Map<List<Responses.VoucherResponse>>(transaction.VoucherTransaction.Select(x => x.Voucher)) : null;
        var serviceResponse = transaction.ServiceTransactions.Any() ? mapper.Map<Responses.ServiceResponse>(transaction.ServiceTransactions.FirstOrDefault().Service) : null;
        var productResponses = transaction.ProductTransactions.Any() ? mapper.Map<List<Responses.ProductResponse>>(transaction.ProductTransactions.Select(x => x.Product)) : null;
        var devices = transaction.Devices.Any() ? mapper.Map<List<Responses.DeviceResponse>>(transaction.Devices) : null;

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
            Vouchers = voucherResponses,
            Service = serviceResponse,
            Products = productResponses,
            Devices = devices
        };

        return Result.Success(response);
    }
}
