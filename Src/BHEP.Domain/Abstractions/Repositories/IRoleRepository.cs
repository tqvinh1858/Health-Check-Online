using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IRoleRepository : IRepositoryBase<Role, int>
{
    Task<bool> IsAdmin(int roleId);
}
