using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using static BHEP.Contract.Services.V1.Service.Responses;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class ServiceRepository : RepositoryBase<Service, int>, IServiceRepository
{
    private readonly ApplicationDbContext context;
    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<List<ServiceGetAllResponse>> GetTopRatedServices(int number)
    {
        var topRatedServices = await context.Service
            .Include(s => s.ServiceRates)
            .Select(s => new
            {
                Service = s,
                AverageRate = s.ServiceRates.Any() ? s.ServiceRates.Average(sr => sr.Rate) : 0
            })
            .OrderByDescending(s => s.AverageRate)
            .Take(number)
            .ToListAsync();

        var ServiceResponses = topRatedServices.Select(p => new ServiceGetAllResponse(
                p.Service.Id,
                p.Service.Image,
                p.Service.Name,
                p.Service.Type,
                p.Service.Description,
                p.Service.Price,
                p.Service.Duration,
                p.AverageRate
            )).ToList();

        return ServiceResponses;
    }
}
