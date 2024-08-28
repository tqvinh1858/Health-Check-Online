using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.AppointmentRepo;
public class SymptomRepository : RepositoryBase<Symptom, int>, ISymptomRepository
{
    private readonly ApplicationDbContext context;
    public SymptomRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public IQueryable<Symptom> GetSymptomsForSpecialist(int specialistId)
    {
        var symptoms = context.SpecialistSymptom
            .Where(ss => ss.SpecialistId == specialistId)
            .Select(ss => ss.Symptom).AsNoTracking();

        return symptoms;
    }
}
