using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Persistence.Repositories.UserRepo;
public class UserCodeRepository : IUserCodeRepository
{
    private readonly ApplicationDbContext context;

    public UserCodeRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Add(UserCode userCode)
    {
        await context.UserCode.AddAsync(userCode);
    }
}
