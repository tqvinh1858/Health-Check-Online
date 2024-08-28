namespace BHEP.Domain.Entities.AppointmentEntities;
public class AppointmentSymptom
{
    public int AppointmentId { get; set; }
    public int SymptomId { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
    public virtual Symptom Symptom { get; set; } = null!;
}
