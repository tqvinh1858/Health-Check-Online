using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;


namespace BHEP.Application.Usecases.V1.Payment.Commands;
public sealed class UpdatePaymentCommandHandler : ICommandHandler<Command.UpdatePaymentCommand, Responses.PaymentResponse>
{
    private readonly IMapper mapper;
    private readonly IPaymentRepository paymentRepository;
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly IUserRepository userRepository;
    private readonly ApplicationDbContext context;

    public UpdatePaymentCommandHandler(
        IMapper mapper,
        IPaymentRepository paymentRepository,
        ICoinTransactionRepository coinTransactionRepository,
        IUserRepository userRepository,
        ApplicationDbContext context)
    {
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
        this.coinTransactionRepository = coinTransactionRepository;
        this.userRepository = userRepository;
        this.context = context;
    }

    public async Task<Result<Responses.PaymentResponse>> Handle(Command.UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.FindByIdAsync(request.Id.Value, cancellationToken)
            ?? throw new PaymentException.PaymentIdNotFoundException();

        if (payment.Status != PaymentStatus.Pending)
        {
            throw new PaymentException.PaymentBadRequestException("Payment is not pending.");
        }

        try
        {
            payment.Status = request.Status;

            paymentRepository.Update(payment);

            var resultResponse = mapper.Map<Responses.PaymentResponse>(payment);

            if (request.Status == PaymentStatus.Completed)
            {
                await HandleCoinTransaction(payment);
            }

            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private async Task HandleCoinTransaction(Domain.Entities.SaleEntities.Payment payment)
    {
        var user = await userRepository.FindByIdAsync(payment.UserId)
            ?? throw new Exception("User not found.");

        user.Balance += payment.Amount;
        userRepository.Update(user);

        var newCoinTransaction = new Domain.Entities.SaleEntities.CoinTransaction
        {
            UserId = payment.UserId,
            Amount = payment.Amount,
            IsMinus = false,
            Title = "Payment Successfully",
            Description = payment.Description,
            Type = CoinTransactionType.Deposit
        };

        coinTransactionRepository.Add(newCoinTransaction);
        await context.SaveChangesAsync();
    }

}
