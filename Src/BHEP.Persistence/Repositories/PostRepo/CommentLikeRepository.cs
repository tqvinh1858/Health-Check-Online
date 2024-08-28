using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.PostRepo;
public class CommentLikeRepository : RepositoryBase<CommentLike, int>, ICommentLikeRepository
{
    private readonly ApplicationDbContext context;
    public CommentLikeRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExist(int UserId, int CommentId)
    => await context.CommentLike.AnyAsync(x => x.UserId == UserId && x.CommentId == CommentId);

    public async Task<int> GetTotalLike(int CommentId)
        => await context.CommentLike.Where(x => x.CommentId == CommentId).CountAsync();
}
