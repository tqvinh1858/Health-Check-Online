using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Domain.Entities.PostEntities;
public class PostSpecialist
{
    public int PostId { get; set; }
    public int SpecialistId { get; set; }

    public virtual Post Post { get; set; } = null!;
    public virtual Specialist Specialist { get; set; } = null!;
}
