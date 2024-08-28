using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class CodeRepository : RepositoryBase<Code, int>, ICodeRepository
{
    public CodeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
