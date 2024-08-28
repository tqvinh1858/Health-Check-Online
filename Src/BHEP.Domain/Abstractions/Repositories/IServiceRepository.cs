using BHEP.Domain.Entities.SaleEntities;
using static BHEP.Contract.Services.V1.Service.Responses;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IServiceRepository : IRepositoryBase<Service, int>
{
    Task<List<ServiceGetAllResponse>> GetTopRatedServices(int number);
}
