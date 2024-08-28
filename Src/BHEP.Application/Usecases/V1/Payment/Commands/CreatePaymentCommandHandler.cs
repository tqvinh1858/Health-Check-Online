using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Infrastructure.VnPay.Respository.IRepository;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.Payment.Commands;
public sealed class CreatePaymentCommandHandler : ICommandHandler<Command.CreatePaymentCommand, Responses.PaymentResponse>
{

    private readonly IPaymentRepository paymentRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IVnPayRepository vnPayRepository;

    public CreatePaymentCommandHandler(
        IPaymentRepository paymentRepository,
        ApplicationDbContext context,
        IMapper mapper,
        IVnPayRepository vnPayRepository)
    {
        this.paymentRepository = paymentRepository;
        this.context = context;
        this.mapper = mapper;
        this.vnPayRepository = vnPayRepository;
    }

    public async Task<Result<Responses.PaymentResponse>> Handle(Command.CreatePaymentCommand request, CancellationToken cancellationToken)
    {

        var payment = new Domain.Entities.SaleEntities.Payment
        {
            UserId = request.UserId,
            Method = "VnPay",
            Amount = request.Amount,
            Status = PaymentStatus.Pending,
            CreatedDate = TimeZones.SoutheastAsia
        };

        context.Payment.Add(payment);
        await context.SaveChangesAsync();

        var paymentUrl = vnPayRepository.CreatePaymentUrl((decimal)request.Amount, "Payment for order", payment.Id.ToString(), "127.0.0.1");
        if (paymentUrl == null)
        {
            return (Result<Responses.PaymentResponse>)Result<Responses.PaymentResponse>.Failure("Failed to generate payment URL");
        }

        var response = new Responses.PaymentResponse(payment.Id, payment.UserId, payment.Method, paymentUrl);

        return Result.Success(response, 201);
    }
}
