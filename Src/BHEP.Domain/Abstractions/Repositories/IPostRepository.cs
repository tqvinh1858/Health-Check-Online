using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Persistence.Repositories.Interface;
public interface IPostRepository : IRepositoryBase<Post, int>
{
    void Delete(Post post);
    Task<bool> IsExist(int id);
    Task<int> GetTotalCommentAndReply(int id, CancellationToken cancellationToken = default);
    Task<List<Comment>> GetComments(int id, CancellationToken cancellationToken = default);
    Task<List<Specialist>> GetSpecialist(int id, CancellationToken cancellationToken = default);

}
