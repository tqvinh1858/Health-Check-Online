using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Persistence.Repositories.AppointmentRepo;
public class SpecialistSymptomRepository : ISpecialistSymptomRepository
{

    private readonly ApplicationDbContext context;

    public SpecialistSymptomRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Add(SpecialistSymptom entity)
    {
        await context.SpecialistSymptom.AddRangeAsync(entity);
    }
}
