using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.AppointmentEntities;
public class Appointment : EntityAuditBase<int>
{
    public Appointment()
    {
        AppointmentSymptoms = new HashSet<AppointmentSymptom>();
    }

    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public decimal Price { get; set; }
    public string? Address { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? Description { get; set; }
    public string? Note { get; set; }
    public AppointmentStatus Status { get; set; }

    public virtual User Customer { get; set; } = null!;
    public virtual User Employee { get; set; } = null!;
    public virtual UserRate UserRate { get; set; } = null!;
    public virtual ICollection<AppointmentSymptom> AppointmentSymptoms { get; set; }

    public void Update(
        DateTime Date,
        decimal Price,
        string? Address,
        string? Latitude,
        string? Longitude,
        string? Description,
        string? Note,
        AppointmentStatus Status)
    {
        this.Date = Date;
        this.Price = Price;
        this.Address = Address;
        this.Latitude = Latitude;
        this.Longitude = Longitude;
        this.Description = Description;
        this.Note = Note;
        this.Status = Status;
        UpdatedDate = DateTime.Now;
    }
}
