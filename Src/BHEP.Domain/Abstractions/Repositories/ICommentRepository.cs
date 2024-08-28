using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface ICommentRepository : IRepositoryBase<Comment, int>
{
    Task<bool> IsExist(int CommentId);
}
