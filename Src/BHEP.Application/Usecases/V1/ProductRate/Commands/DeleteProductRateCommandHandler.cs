using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ProductRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.ProductRate.Commands;
public sealed class DeleteProductRateCommandHandler : ICommandHandler<Command.DeleteProductRateCommand>
{
    private readonly IProductRateRepository ProductRateRepository;
    public DeleteProductRateCommandHandler(
        IProductRateRepository ProductRateRepository)
    {
        this.ProductRateRepository = ProductRateRepository;
    }
    public async Task<Result> Handle(Command.DeleteProductRateCommand request, CancellationToken cancellationToken)
    {
        var ProductRateExist = await ProductRateRepository.FindByIdAsync(request.Id)
            ?? throw new ProductRateException.ProductRateIdNotFoundException();

        try
        {
            ProductRateRepository.Delete(ProductRateExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
