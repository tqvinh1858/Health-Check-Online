using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.AppointmentRepo;
public class AppointmentSymptomRepository : IAppointmentSymptomRepository
{
    private readonly ApplicationDbContext context;

    public AppointmentSymptomRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Add(List<AppointmentSymptom> appointmentSymptom)
    {
        await context.AppointmentSymptom.AddRangeAsync(appointmentSymptom);
    }

    public async Task Delete(int appointmentId)
    {
        var appointmentSymptoms = await context.AppointmentSymptom
            .Where(ap => ap.AppointmentId == appointmentId).ToListAsync();

        if (appointmentSymptoms != null)
        {
            context.AppointmentSymptom.RemoveRange(appointmentSymptoms);
        }
    }
}
