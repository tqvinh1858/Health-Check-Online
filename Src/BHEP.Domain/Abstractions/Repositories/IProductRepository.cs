using BHEP.Contract.Services.V1.Product;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IProductRepository : IRepositoryBase<Product, int>
{
    Task<List<Responses.ProductGetAllResponse>> GetTopRatedProducts(int number);

}
