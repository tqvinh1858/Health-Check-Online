using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.PostRepo;
public class ReplyLikeRepository : RepositoryBase<ReplyLike, int>, IReplyLikeRepository
{
    private readonly ApplicationDbContext context;
    public ReplyLikeRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExist(int UserId, int ReplyId)
    => await context.ReplyLike.AnyAsync(x => x.UserId == UserId && x.ReplyId == ReplyId);

    public async Task<int> GetTotalLike(int ReplyId)
        => await context.ReplyLike.Where(x => x.ReplyId == ReplyId).CountAsync();
}
