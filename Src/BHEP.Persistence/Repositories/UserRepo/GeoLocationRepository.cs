using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Persistence.Repositories.UserRepo;
public class GeoLocationRepository : RepositoryBase<GeoLocation, int>, IGeoLocationRepository
{
    public GeoLocationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
