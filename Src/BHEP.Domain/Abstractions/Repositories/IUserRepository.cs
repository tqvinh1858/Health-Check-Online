using System.Security.Claims;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IUserRepository : IRepositoryBase<User, int>
{
    void Delete(User customer);
    Task<User?> FindByEmailAsync(string email);
    string HashPassword(string password);
    bool VerifyPassword(string hashPassword, string inputPassword);
    Task<IEnumerable<Claim>> GenerateClaims(User customer, string roleName);
    Task<bool> IsExist(int Id);
}
