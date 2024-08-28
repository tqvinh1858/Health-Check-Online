using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IAppointmentSpecialistRepository
{
    Task Add(List<AppointmentSymptom> appointmentSymptom);
    Task Delete(int appointmentId);
}
