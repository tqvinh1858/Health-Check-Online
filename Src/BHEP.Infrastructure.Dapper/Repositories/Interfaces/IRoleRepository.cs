using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Infrastructure.Dapper.Repositories.Interfaces;
public interface IRoleRepository : IGenericRepository<Role, int>
{
    Task<bool> IsRoleExist(string Name);
}
