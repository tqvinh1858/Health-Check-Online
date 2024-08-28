using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Voucher;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Voucher.Commands;
public sealed class UpdateVoucherCommandHandler : ICommandHandler<Command.UpdateVoucherCommand, Responses.VoucherResponse>
{
    private readonly IVoucherRepository voucherRepository;
    private readonly IMapper mapper;

    public UpdateVoucherCommandHandler(
        IVoucherRepository voucherRepository,
        IMapper mapper)
    {
        this.voucherRepository = voucherRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.VoucherResponse>> Handle(Command.UpdateVoucherCommand request, CancellationToken cancellationToken)
    {
        var voucher = await voucherRepository.FindByIdAsync(request.Id, cancellationToken)
           ?? throw new VoucherException.VoucherIdNotFoundException();

        try
        {
            voucher.Update(request.Name, request.Code, request.Discount, request.Stock, request.StartDate, request.EndDate);
            voucherRepository.Update(voucher);

            var resultResponse = mapper.Map<Responses.VoucherResponse>(voucher);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
