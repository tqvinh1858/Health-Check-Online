using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.PostRepo;
public class CommentRepository : RepositoryBase<Comment, int>, ICommentRepository
{
    private readonly ApplicationDbContext context;
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExist(int CommentId)
        => await context.Comment.AnyAsync(x => x.Id == CommentId);
}
