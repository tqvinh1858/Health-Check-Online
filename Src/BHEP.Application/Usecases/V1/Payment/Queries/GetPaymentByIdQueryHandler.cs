using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Payment.Queries;
public sealed class GetPaymentByIdQueryHandler : IQueryHandler<Query.GetPaymentByIdQuery, Responses.PaymentResponse>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetPaymentByIdQueryHandler(
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.PaymentResponse>> Handle(Query.GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await paymentRepository.FindSingleAsync(
            predicate: x => x.IsDeleted == false && x.Id == request.Id,
            cancellationToken);

        var resultResponse = mapper.Map<Responses.PaymentResponse>(result);
        return Result.Success(resultResponse);
    }



}
