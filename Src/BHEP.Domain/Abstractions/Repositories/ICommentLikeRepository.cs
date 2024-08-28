using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface ICommentLikeRepository : IRepositoryBase<CommentLike, int>
{
    Task<bool> IsExist(int UserId, int CommentId);
    Task<int> GetTotalLike(int CommentId);
}
