using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IReplyLikeRepository : IRepositoryBase<ReplyLike, int>
{
    Task<bool> IsExist(int UserId, int ReplyId);
    Task<int> GetTotalLike(int ReplyId);
}
