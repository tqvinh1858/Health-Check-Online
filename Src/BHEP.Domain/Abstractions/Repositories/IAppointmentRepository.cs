using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IAppointmentRepository : IRepositoryBase<Appointment, int>
{
    Task<bool> IsExistAppointment(int customerId, int employeeId, int appointmentId);
    bool IsInRange(double range, double lat1, double lon1, double lat2, double lon2);
    void Delete(Appointment appointment);
    int CompletedByEmployeeId(int employeeId);
}
