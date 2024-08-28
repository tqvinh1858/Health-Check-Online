using BHEP.Domain.Entities.PostEntities;
using BHEP.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.PostRepo;
public class ReplyRepository : RepositoryBase<Reply, int>, IReplyRepository
{
    private readonly ApplicationDbContext context;
    public ReplyRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }
    public async Task<bool> IsExist(int ReplyId)
    => await context.Reply.AnyAsync(x => x.Id == ReplyId);
}
