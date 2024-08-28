using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Payment.Commands;
public sealed class DeletePaymentCommandHandler : ICommandHandler<Command.DeletePaymentCommand>
{
    private readonly IMapper mapper;
    private readonly IPaymentRepository paymentRepository;

    public DeletePaymentCommandHandler(
        IMapper mapper,
        IPaymentRepository paymentRepository)
    {
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
    }

    public async Task<Result> Handle(Command.DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var Payment = await paymentRepository.FindByIdAsync(request.Id)
            ?? throw new PaymentException.PaymentIdNotFoundException();

        try
        {
            paymentRepository.Delete(Payment);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
