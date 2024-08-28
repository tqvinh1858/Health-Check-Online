using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Domain.Entities.UserEntities;
public class UserRate : EntityAuditBase<int>
{
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int AppointmentId { get; set; }
    public string? Reason { get; set; }
    public float Rate { get; set; }


    public virtual User Customer { get; set; }
    public virtual User Employee { get; set; }
    public virtual Appointment Appointment { get; set; }
}
