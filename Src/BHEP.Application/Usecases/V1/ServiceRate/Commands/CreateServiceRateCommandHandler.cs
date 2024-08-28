using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ServiceRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.ServiceRate.Commands;
public sealed class CreateServiceRateCommandHandler : ICommandHandler<Command.CreateServiceRateCommand, Responses.CreateServiceRateResponse>
{
    private readonly IServiceRateRepository ServiceRateRepository;
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateServiceRateCommandHandler(
        IServiceRateRepository ServiceRateRepository,
        ApplicationDbContext context,
        IMapper mapper,
        ICoinTransactionRepository coinTransactionRepository)
    {
        this.ServiceRateRepository = ServiceRateRepository;
        this.context = context;
        this.mapper = mapper;
        this.coinTransactionRepository = coinTransactionRepository;
    }
    public async Task<Result<Responses.CreateServiceRateResponse>> Handle(Command.CreateServiceRateCommand request, CancellationToken cancellationToken)
    {
        if (!await coinTransactionRepository.IsExistServiceTransaction(request.TransactionId, request.ServiceId))
            throw new ServiceRateException.ServiceRateBadRequestException("Transaction Is Not Exist!");

        if (await ServiceRateRepository.IsExistServiceRate(request.UserId, request.TransactionId, request.ServiceId))
            throw new ServiceRateException.ServiceRateBadRequestException("Has been Rating!");


        try
        {
            var ServiceRate = new Domain.Entities.SaleEntities.ServiceRate
            {
                UserId = request.UserId,
                ServiceId = request.ServiceId,
                TransactionId = request.TransactionId,
                Reason = request.Reason,
                Rate = request.Rate,
            };

            ServiceRateRepository.Add(ServiceRate);
            await context.SaveChangesAsync();

            var response = mapper.Map<Responses.CreateServiceRateResponse>(ServiceRate);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
