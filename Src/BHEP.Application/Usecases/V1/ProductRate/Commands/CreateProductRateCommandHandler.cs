using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ProductRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.ProductRate.Commands;
public sealed class CreateProductRateCommandHandler : ICommandHandler<Command.CreateProductRateCommand, Responses.CreateProductResponse>
{
    private readonly IProductRateRepository ProductRateRepository;
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateProductRateCommandHandler(
        IProductRateRepository ProductRateRepository,
        ApplicationDbContext context,
        IMapper mapper,
        ICoinTransactionRepository coinTransactionRepository)
    {
        this.ProductRateRepository = ProductRateRepository;
        this.context = context;
        this.mapper = mapper;
        this.coinTransactionRepository = coinTransactionRepository;
    }
    public async Task<Result<Responses.CreateProductResponse>> Handle(Command.CreateProductRateCommand request, CancellationToken cancellationToken)
    {
        if (!await coinTransactionRepository.IsExistProductTransaction(request.TransactionId, request.ProductId))
            throw new ProductRateException.ProductRateBadRequestException("Payment Is Not Exist!");

        if (await ProductRateRepository.IsExistProductRate(request.UserId, request.TransactionId, request.ProductId))
            throw new ProductRateException.ProductRateBadRequestException("Has been Rating!");

        try
        {
            var ProductRate = new Domain.Entities.SaleEntities.ProductRate
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                TransactionId = request.TransactionId,
                Reason = request.Reason,
                Rate = request.Rate,
            };

            ProductRateRepository.Add(ProductRate);
            await context.SaveChangesAsync();

            var response = mapper.Map<Responses.CreateProductResponse>(ProductRate);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
