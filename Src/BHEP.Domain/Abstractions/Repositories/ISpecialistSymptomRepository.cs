using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface ISpecialistSymptomRepository
{
    Task Add(SpecialistSymptom entity);
}
