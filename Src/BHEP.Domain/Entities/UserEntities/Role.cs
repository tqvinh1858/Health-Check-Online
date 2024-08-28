using BHEP.Contract.Services.V2.Role;
using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.UserEntities;
public class Role : EntityBase<int>
{
    public Role()
    {
        Users = new HashSet<User>();
    }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<User> Users { get; set; }

    public void Update(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }

    public static implicit operator Responses.RoleResponse(Role role)
        => new Responses.RoleResponse(role.Id, role.Name, role.Description);
}
