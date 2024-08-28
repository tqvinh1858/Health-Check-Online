using System.Security.Claims;
using System.Security.Cryptography;
using BHEP.Contract.Constants;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.UserRepo;
public class UserRepository : RepositoryBase<User, int>, IUserRepository
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char Delimiter = ';';
    private readonly ApplicationDbContext context;
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }
    

    public void Delete(User customer)
    {
        customer.DeletedDate = TimeZones.SoutheastAsia;
        customer.IsDeleted = true;
        Update(customer);
    }
    public async Task<User?> FindByEmailAsync(string email)
    {
        var customer = await context.User.AsTracking().FirstOrDefaultAsync(x => x.Email == email);
        return customer;
    }

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithmName, KeySize);

        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool VerifyPassword(string hashPassword, string inputPassword)
    {
        var elements = hashPassword.Split(Delimiter);
        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, hashAlgorithmName, KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }

    public async Task<IEnumerable<Claim>> GenerateClaims(User customer, string roleName)
    {

        //Generate claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim(ClaimTypes.Role, roleName.ToLower())
        };
        return claims;
    }

    //public async Task<bool> IsExistUser(int Id)
    //    => await context.User.FindAsync(Id) != null;

    public async Task<bool> IsExist(int Id)
        => await context.User.AnyAsync(x => x.Id == Id);
}
