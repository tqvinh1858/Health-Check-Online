using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.ProductRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.ProductRate.Commands;
public sealed class UpdateProductRateCommandHandler : ICommandHandler<Command.UpdateProductRateCommand, Responses.ProductRateResponse>
{
    private readonly IProductRateRepository ProductRateRepository;
    private readonly IMapper mapper;
    public UpdateProductRateCommandHandler(
        IProductRateRepository ProductRateRepository,
        IMapper mapper)
    {
        this.ProductRateRepository = ProductRateRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.ProductRateResponse>> Handle(Command.UpdateProductRateCommand request, CancellationToken cancellationToken)
    {
        var ProductRateExist = await ProductRateRepository.FindByIdAsync(request.Id.Value)
            ?? throw new ProductRateException.ProductRateIdNotFoundException();

        try
        {
            ProductRateExist.Reason = request.Reason;
            ProductRateExist.Rate = request.Rate;
            ProductRateExist.UpdatedDate = TimeZones.SoutheastAsia;

            ProductRateRepository.Update(ProductRateExist);
            var ProductRateResponse = mapper.Map<Responses.ProductRateResponse>(ProductRateExist);

            return Result.Success(ProductRateResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
