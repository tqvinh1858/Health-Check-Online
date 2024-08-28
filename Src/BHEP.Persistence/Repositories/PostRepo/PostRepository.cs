using BHEP.Contract.Constants;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Domain.Entities.PostEntities;
using BHEP.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;


namespace BHEP.Persistence.Repositories.PostRepo;
public class PostRepository : RepositoryBase<Post, int>, IPostRepository
{
    private readonly ApplicationDbContext context;

    public PostRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public void Delete(Post post)
    {
        post.DeletedDate = TimeZones.SoutheastAsia;
        post.IsDeleted = true;
        Update(post);
    }

    public async Task<List<Comment>> GetComments(int postId, CancellationToken cancellationToken = default)
    {
        var comments = await context.Comment
            .Where(c => c.PostId == postId && !c.IsDeleted)
            .AsSplitQuery()
            .Include(c => c.User)
            .ToListAsync(cancellationToken);

        return comments;
    }

    public async Task<List<Specialist>> GetSpecialist(int postId, CancellationToken cancellationToken = default)
    {
        var specialists = await context.PostSpecialist
            .Where(x => x.PostId == postId)
            .AsSplitQuery()
            .Include(x => x.Specialist)
            .Select(x => x.Specialist)
            .ToListAsync(cancellationToken);

        return specialists;
    }

    public async Task<bool> IsExist(int id)
        => await context.Post.AnyAsync(x => x.Id == id);
    public async Task<int> GetTotalCommentAndReply(int id, CancellationToken cancellationToken = default)
    {
        var listComment = await context.Post.Where(x => x.Id == id)
            .SelectMany(x => x.Comments)
            .ToListAsync(cancellationToken);
        if (!listComment.Any())
            return 0;
        int count = listComment.Count() + listComment.Sum(x => x.Replies?.Count ?? 0);

        return count;
    }
}
