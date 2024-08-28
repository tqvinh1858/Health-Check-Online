using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface ISymptomRepository : IRepositoryBase<Symptom, int>
{
    IQueryable<Symptom> GetSymptomsForSpecialist(int specialistId);
}
