using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Voucher;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Voucher.Queries;
public sealed class GetVoucherByIdQueryHandler : IQueryHandler<Query.GetVoucherByIdQuery, Responses.VoucherResponse>
{
    private readonly IVoucherRepository voucherRepository;

    public GetVoucherByIdQueryHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }

    public async Task<Result<Responses.VoucherResponse>> Handle(Query.GetVoucherByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await voucherRepository.FindByIdAsync(request.Id, cancellationToken)
                 ?? throw new DeviceException.DeviceIdNotFoundException();

        var resultResponse = new Responses.VoucherResponse(
            result.Id,
            result.Name,
            result.Code,
            result.Discount,
            result.Stock,
            result.IsExpired,
            result.OutOfStock
          );

        return Result.Success(resultResponse);
    }
}
