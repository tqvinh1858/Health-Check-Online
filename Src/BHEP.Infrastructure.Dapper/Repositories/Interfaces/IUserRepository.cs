using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Infrastructure.Dapper.Repositories.Interfaces;
public interface IUserRepository : IGenericRepository<User, int>
{
}
