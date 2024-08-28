using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities;
public class Major : EntityBase<int>
{
    public Major()
    {
        WorkProfiles = new HashSet<WorkProfile>();
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<WorkProfile> WorkProfiles { get; set; }



    public void Update(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }

}
