using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.BlogEntities;

namespace BHEP.Persistence.Repositories.BlogRepo;
public class BlogPhotoRepository : RepositoryBase<BlogPhoto, int>, IBlogPhotoRepository
{
    public BlogPhotoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
