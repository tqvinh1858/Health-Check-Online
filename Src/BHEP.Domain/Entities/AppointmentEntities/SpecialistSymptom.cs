namespace BHEP.Domain.Entities.AppointmentEntities;
public class SpecialistSymptom
{
    public int SpecialistId { get; set; }
    public int SymptomId { get; set; }

    public virtual Specialist Specialist { get; set; } = null!;
    public virtual Symptom Symptom { get; set; } = null!;
}
