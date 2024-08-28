using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Persistence.Repositories.Interface;
public interface IReplyRepository : IRepositoryBase<Reply, int>
{
    Task<bool> IsExist(int ReplyId);
}
