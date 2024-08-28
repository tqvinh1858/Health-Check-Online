using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Voucher;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Voucher.Commands;
public sealed class DeleteVoucherCommandHandler : ICommandHandler<Command.DeleteVoucherCommand>
{
    private readonly IVoucherRepository voucherRepository;

    public DeleteVoucherCommandHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }

    public async Task<Result> Handle(Command.DeleteVoucherCommand request, CancellationToken cancellationToken)
    {
        var voucherExist = await voucherRepository.FindByIdAsync(request.Id, cancellationToken)
                               ?? throw new VoucherException.VoucherIdNotFoundException();

        try
        {
            voucherRepository.Remove(voucherExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
