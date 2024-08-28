using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.AppointmentRepo;
public class AppointmentRepository : RepositoryBase<Appointment, int>, IAppointmentRepository
{
    private readonly ApplicationDbContext context;
    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExistAppointment(int customerId, int employeeId, int appointmentId)
    => await context.Appointment.FirstOrDefaultAsync(x =>
    x.CustomerId == customerId && x.Id == appointmentId && x.EmployeeId == employeeId) is null ? false : true;

    public bool IsInRange(double range, double lat1, double lon1, double lat2, double lon2)
    {
        const int r = 6371; // km
        const double p = Math.PI / 180;

        double a = 0.5 - Math.Cos((lat2 - lat1) * p) / 2
                      + Math.Cos(lat1 * p) * Math.Cos(lat2 * p) *
                        (1 - Math.Cos((lon2 - lon1) * p)) / 2;

        var distance = 2 * r * Math.Asin(Math.Sqrt(a));

        return distance < range;
    }

    public void Delete(Appointment appointment)
    {
        appointment.IsDeleted = true;
        appointment.DeletedDate = TimeZones.SoutheastAsia;
        Update(appointment);
    }

    public int CompletedByEmployeeId(int employeeId)
    {
        var count = FindAll(x => x.EmployeeId == employeeId && x.Status.Equals(AppointmentStatus.Completed)).Count();

        return count;
    }
}
