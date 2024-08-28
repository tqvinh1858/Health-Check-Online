using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Voucher;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Voucher.Commands;
public sealed class CreateVoucherCommandHandler : ICommandHandler<Command.CreateVoucherCommand, Responses.VoucherResponse>
{
    private readonly IVoucherRepository voucherRepository;
    private readonly IMapper mapper;

    public CreateVoucherCommandHandler(
        IVoucherRepository voucherRepository,
        IMapper mapper)
    {
        this.voucherRepository = voucherRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.VoucherResponse>> Handle(Command.CreateVoucherCommand request, CancellationToken cancellationToken)
    {
        var voucher = new Domain.Entities.SaleEntities.Voucher
        {
            Name = request.Name,
            Code = request.Code,
            Discount = request.Discount,
            Stock = request.Stock,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        try
        {
            voucherRepository.Add(voucher);

            var resultResponse = mapper.Map<Responses.VoucherResponse>(voucher);

            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}


