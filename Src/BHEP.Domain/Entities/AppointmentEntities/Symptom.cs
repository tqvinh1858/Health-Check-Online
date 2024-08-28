using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.AppointmentEntities;
public class Symptom : EntityBase<int>
{
    public Symptom()
    {
        AppointmentSymptoms = new HashSet<AppointmentSymptom>();
        SpecialistSymptoms = new HashSet<SpecialistSymptom>();
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<AppointmentSymptom> AppointmentSymptoms { get; set; }
    public virtual ICollection<SpecialistSymptom> SpecialistSymptoms { get; set; }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
