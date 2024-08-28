using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Product;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.Product.Queries;
public sealed class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductByIdQuery, Responses.ProductGetByIdResponse>
{
    private readonly IProductRepository productRepository;
    private readonly IProductRateRepository productRateRepository;
    private readonly IMapper mapper;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository,
        IMapper mapper,
        IProductRateRepository productRateRepository)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        this.productRateRepository = productRateRepository;
    }

    public async Task<Result<Responses.ProductGetByIdResponse>> Handle(Query.GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.FindByIdAsync(request.Id, cancellationToken)
           ?? throw new ProductException.ProductIdNotFoundException();

        // GetListRating
        var listRating = await productRateRepository.FindAll(predicate: x => x.ProductId.Equals(result.Id)).ToListAsync();

        // GetAvgRating
        var avgRate = await productRateRepository.GetAvgRating(result.Id);

        var resultResponse = new Responses.ProductGetByIdResponse(
            result.Id,
            result.Name,
            result.Image,
            result.Description,
            result.Price,
            result.Stock,
            avgRate,
            mapper.Map<List<Contract.Services.V1.ProductRate.Responses.ProductRateResponse>>(listRating));

        return Result.Success(resultResponse);
    }
}
