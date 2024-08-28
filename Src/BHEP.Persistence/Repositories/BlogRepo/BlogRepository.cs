using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.BlogEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.BlogRepo;
public class BlogRepository : RepositoryBase<Blog, int>, IBlogRepository
{
    private readonly ApplicationDbContext context;
    public BlogRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public new async Task<Blog> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var blog = await context.Blog.AsTracking()
            .Include(x => x.User)
            .Include(x => x.BlogPhotos)
            .Include(x => x.BlogRates)
            .Include(x => x.BlogTopics).ThenInclude(x => x.Topic)
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return blog;
    }

    public async Task<List<Topic>> GetTopics(int id)
    {
        var listTopics = await context.BlogTopic
            .Where(x => x.BlogId == id)
            .AsSplitQuery()
            .Include(x => x.Topic)
            .Select(x => x.Topic)
            .ToListAsync();

        return listTopics;
    }

    public async Task<List<BlogPhoto>> GetPhotos(int id)
    {
        var listPhotos = await context.BlogPhoto
            .Where(x => x.BlogId == id)
            .ToListAsync();
        return listPhotos;
    }
}
