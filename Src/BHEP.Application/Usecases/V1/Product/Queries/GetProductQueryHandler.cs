using System.Linq.Expressions;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Product;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Product.Queries;
public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, PagedResult<Responses.ProductGetAllResponse>>
{
    private readonly IProductRepository productRepository;
    private readonly IProductRateRepository productRateRepository;

    public GetProductQueryHandler(
        IProductRepository productRepository,
        IProductRateRepository productRateRepository)
    {
        this.productRepository = productRepository;
        this.productRateRepository = productRateRepository;
    }

    public async Task<Result<PagedResult<Responses.ProductGetAllResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Domain.Entities.SaleEntities.Product> EventsQuery;

            EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
                ? productRepository.FindAll()
                : productRepository.FindAll(x => x.Name.Contains(request.searchTerm));


            var keySelector = GetSortProperty(request);

            EventsQuery = request.sortOrder == SortOrder.Descending
              ? EventsQuery.OrderByDescending(keySelector)
              : EventsQuery.OrderBy(keySelector);


            var Events = await PagedResult<Domain.Entities.SaleEntities.Product>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            List<Responses.ProductGetAllResponse> productGetAllResponses = new List<Responses.ProductGetAllResponse>();

            foreach (var item in Events.items)
            {
                var productGetAllResponse = new Responses.ProductGetAllResponse(
                    item.Id,
                    item.Name,
                    item.Image,
                    item.Description,
                    item.Price,
                    item.Stock,
                    await productRateRepository.GetAvgRating(item.Id)
                    );

                productGetAllResponses.Add(productGetAllResponse);
            }

            var result = PagedResult<Responses.ProductGetAllResponse>.Create(
                productGetAllResponses,
                Events.pageIndex,
                Events.pageSize,
                EventsQuery.Count()
                );

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private static Expression<Func<Domain.Entities.SaleEntities.Product, object>> GetSortProperty(Query.GetProductQuery request)
       => request.sortColumn?.ToLower() switch
       {
           "name" => e => e.Name,
           "price" => e => e.Price,
           _ => e => e.Name
       };
}
