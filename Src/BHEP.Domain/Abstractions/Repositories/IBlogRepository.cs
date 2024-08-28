using BHEP.Domain.Entities.BlogEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IBlogRepository : IRepositoryBase<Blog, int>
{
    Task<Blog> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Topic>> GetTopics(int id);
    Task<List<BlogPhoto>> GetPhotos(int id);
}
