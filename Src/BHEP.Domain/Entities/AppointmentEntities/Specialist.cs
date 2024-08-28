using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.PostEntities;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.AppointmentEntities;
public class Specialist : EntityBase<int>
{
    public Specialist()
    {
        Employees = new HashSet<User>();
        SpecialistsSymptom = new HashSet<SpecialistSymptom>();
        PostSpecialists = new HashSet<PostSpecialist>();
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<User> Employees { get; set; }
    public virtual ICollection<SpecialistSymptom> SpecialistsSymptom { get; set; }
    public virtual ICollection<PostSpecialist> PostSpecialists { get; set; }
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
