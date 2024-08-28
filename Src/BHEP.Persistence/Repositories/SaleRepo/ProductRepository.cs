using BHEP.Contract.Services.V1.Product;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
    private readonly ApplicationDbContext context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<List<Responses.ProductGetAllResponse>> GetTopRatedProducts(int number)
    {
        var topRatedProducts = await context.Product
            .Include(p => p.ProductRates)
            .Select(p => new
            {
                Product = p,
                AverageRate = p.ProductRates.Any() ? p.ProductRates.Average(pr => pr.Rate) : 0
            })
            .OrderByDescending(p => p.AverageRate)
            .Take(number)
            .ToListAsync();

        var productResponses = topRatedProducts.Select(p => new Responses.ProductGetAllResponse(
            p.Product.Id,
            p.Product.Name,
            p.Product.Image,
            p.Product.Description,
            p.Product.Price,
            p.Product.Stock,
            p.AverageRate
        )).ToList();

        return productResponses;
    }
}
