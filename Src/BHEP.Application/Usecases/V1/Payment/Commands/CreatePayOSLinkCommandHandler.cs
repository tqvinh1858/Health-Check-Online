using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.PayOS.Repository.IRepository;
using Net.payOS.Types;
namespace BHEP.Application.Usecases.V1.Payment.Commands;
public sealed class CreatePayOSLinkCommandHandler : ICommandHandler<Command.CreatePayOSLinkCommand, Responses.PayOSResponse>
{
    private readonly IPayOSRepository payOSRepository;
    private readonly IPaymentRepository paymentRepository;
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    public CreatePayOSLinkCommandHandler(
        IPayOSRepository payOSRepository,
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        this.payOSRepository = payOSRepository;
        this.paymentRepository = paymentRepository;
        this.unitOfWork = unitOfWork;
        this.userRepository = userRepository;
    }

    public async Task<Result<Responses.PayOSResponse>> Handle(Command.CreatePayOSLinkCommand request, CancellationToken cancellationToken)
    {
        // Check User
        if (!await userRepository.IsExist(request.UserId))
            throw new PaymentException.PaymentBadRequestException("User Not Found");


        var payment = new Domain.Entities.SaleEntities.Payment
        {
            UserId = request.UserId,
            TransactionId = "",
            Status = PaymentStatus.Pending,
            Method = "PayOS",
            Amount = request.Amount,
            Description = request.Description,
        };

        try
        {
            // Add Payment To GetId
            paymentRepository.Add(payment);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // GetPayOSLink
            var paymentData = new PaymentData(
                  DateTimeOffset.Now.ToUnixTimeMilliseconds() + payment.Id,
                  request.Amount,
                  request.Description,
                  request.items,
                  request.cancelUrl,
                  request.returnUrl,
                  expiredAt: request.ExpiredAt);

            var result = await payOSRepository.CreatePaymentLink(paymentData);

            //Update TransactionId Of Payment
            payment.TransactionId = result.paymentLinkId;
            paymentRepository.Update(payment);



            var response = new Responses.PayOSResponse(
                id: payment.Id,
                accountNumber: result.accountNumber,
                amount: result.amount,
                description: result.description,
                orderCode: result.orderCode,
                currency: result.currency,
                paymentLinkId: result.paymentLinkId,
                status: result.status,
                checkoutUrl: result.checkoutUrl,
                qrCode: result.qrCode
            );


            return response;
        }
        catch (Exception ex)
        {
            await payOSRepository.cancelPaymentLink(payment.Id);
            throw new Exception(ex.Message, ex);
        }


    }
}
