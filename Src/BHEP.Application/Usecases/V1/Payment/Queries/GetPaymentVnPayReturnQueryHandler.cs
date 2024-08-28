using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Payment.Queries;
public sealed class GetPaymentVnPayReturnQueryHandler : IQueryHandler<Query.GetPaymentByIdQuery, Responses.PaymentResponse>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetPaymentVnPayReturnQueryHandler(
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public Task<Result<Responses.PaymentResponse>> Handle(Query.GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
