using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ProductService;
using BHEP.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.ProductService.Queries;
public sealed class GetProductServiceQueryHandler : IQueryHandler<Query.GetProductService, PagedResult<Responses.ProductServiceResponse>>
{
    private readonly IServiceRepository serviceRepository;
    private readonly IProductRepository productRepository;

    public GetProductServiceQueryHandler(
        IServiceRepository serviceRepository,
        IProductRepository productRepository)
    {
        this.serviceRepository = serviceRepository;
        this.productRepository = productRepository;
    }
    public async Task<Result<PagedResult<Responses.ProductServiceResponse>>> Handle(Query.GetProductService request, CancellationToken cancellationToken)
    {
        try
        {
            var listServices = serviceRepository.FindAll();
            var listProducts = await productRepository.FindAll()
                .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize - listServices.Count()).ToListAsync();

            List<Responses.ProductServiceResponse> list = new List<Responses.ProductServiceResponse>();

            foreach (var item in listProducts)
            {
                var OutstandingGetAllResponse = new Responses.ProductServiceResponse(
                    item.Id,
                    item.Name,
                    item.Image,
                    item.Price,
                    "product"
                    );
                list.Add(OutstandingGetAllResponse);
            }

            foreach (var item in listServices)
            {
                var OutstandingGetAllResponse = new Responses.ProductServiceResponse(
                    item.Id,
                    item.Name,
                    item.Image,
                    item.Price,
                    "service"
                    );
                list.Add(OutstandingGetAllResponse);
            }

            var result = PagedResult<Responses.ProductServiceResponse>.Create(
                    list,
                    request.PageIndex,
                    request.PageSize,
                    listServices.Count() + await productRepository.FindAll().CountAsync()
                );

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
