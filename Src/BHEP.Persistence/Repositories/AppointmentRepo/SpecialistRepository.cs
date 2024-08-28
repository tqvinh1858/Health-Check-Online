using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.AppointmentRepo;
public class SpecialistRepository : RepositoryBase<Specialist, int>, ISpecialistRepository
{
    private readonly ApplicationDbContext context;
    public SpecialistRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<List<Specialist>> GetSpecialistsFromListId(List<int> listId)
        => await context.Specialist.Where(x => listId.Contains(x.Id)).ToListAsync();
}
