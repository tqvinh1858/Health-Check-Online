using BHEP.Domain.Entities.BlogEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IBlogRateRepository : IRepositoryBase<BlogRate, int>
{
    void Delete(BlogRate BlogRate);
}
