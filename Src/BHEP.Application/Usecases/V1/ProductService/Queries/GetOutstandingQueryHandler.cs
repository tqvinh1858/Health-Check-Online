using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ProductService;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Outstanding.Queries;
public sealed class GetOutstandingQueryHandler : IQueryHandler<Query.GetOutstandingQuery, PagedResult<Responses.OutstandingResponse>>
{
    private readonly IServiceRepository serviceRepository;
    private readonly IProductRepository productRepository;

    public GetOutstandingQueryHandler(
        IServiceRepository serviceRepository,
        IProductRepository productRepository)
    {
        this.serviceRepository = serviceRepository;
        this.productRepository = productRepository;
    }

    public async Task<Result<PagedResult<Responses.OutstandingResponse>>> Handle(Query.GetOutstandingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var listProducts = await productRepository.GetTopRatedProducts(request.Number);
            var listServices = await serviceRepository.GetTopRatedServices(request.Number);

            List<Responses.OutstandingResponse> OutstandingRateList = new List<Responses.OutstandingResponse>();

            foreach (var item in listProducts)
            {
                var OutstandingGetAllResponse = new Responses.OutstandingResponse(
                    item.Id,
                    item.Name,
                    item.Image,
                    item.Price,
                    item.Rate,
                    "product"
                    );
                OutstandingRateList.Add(OutstandingGetAllResponse);
            }

            foreach (var item in listServices)
            {
                var OutstandingGetAllResponse = new Responses.OutstandingResponse(
                    item.Id,
                    item.Name,
                    item.Image,
                    item.Price,
                    item.Rate,
                    "service"
                    );
                OutstandingRateList.Add(OutstandingGetAllResponse);
            }

            OutstandingRateList = OutstandingRateList.OrderByDescending(x => x.Rate).Take(request.Number).ToList();

            var result = PagedResult<Responses.OutstandingResponse>.Create(
                OutstandingRateList,
                0,
                request.Number,
                0
            );

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
