using BHEP.Contract.Constants;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.BlogEntities;

namespace BHEP.Persistence.Repositories.BlogRepo;
public class BlogRateRepository : RepositoryBase<BlogRate, int>, IBlogRateRepository
{
    public BlogRateRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void Delete(BlogRate BlogRate)
    {
        BlogRate.DeletedDate = TimeZones.SoutheastAsia;
        BlogRate.IsDeleted = true;
        Update(BlogRate);
    }

}
