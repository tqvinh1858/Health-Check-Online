using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.PostRepo;
public class PostLikeRepository : RepositoryBase<PostLike, int>, IPostLikeRepository
{
    private readonly ApplicationDbContext context;
    public PostLikeRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExist(int UserId, int PostId)
        => await context.PostLike.AnyAsync(x => x.UserId == UserId && x.PostId == PostId);

    public async Task<int> GetTotalLike(int PostId)
        => await context.PostLike.Where(x => x.PostId == PostId).CountAsync();

}
