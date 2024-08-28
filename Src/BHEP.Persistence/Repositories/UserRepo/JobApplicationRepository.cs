using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Persistence.Repositories.UserRepo;
public class JobApplicationRepository : RepositoryBase<JobApplication, int>, IJobApplicationRepository
{
    public JobApplicationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
