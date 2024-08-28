using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IPostLikeRepository : IRepositoryBase<PostLike, int>
{
    Task<bool> IsExist(int UserId, int PostId);
    Task<int> GetTotalLike(int PostId);
}
