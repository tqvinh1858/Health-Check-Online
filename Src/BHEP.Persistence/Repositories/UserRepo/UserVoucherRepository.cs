using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Persistence.Repositories.UserRepo;
public class UserVoucherRepository : IUserVoucherRepository
{
    private readonly ApplicationDbContext context;

    public UserVoucherRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
}
