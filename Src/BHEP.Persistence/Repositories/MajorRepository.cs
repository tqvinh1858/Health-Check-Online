using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities;

namespace BHEP.Persistence.Repositories;
public class MajorRepository : RepositoryBase<Major, int>, IMajorRepository
{
    private readonly ApplicationDbContext context;

    public MajorRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;


    }
}
