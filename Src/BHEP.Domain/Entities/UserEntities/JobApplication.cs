using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.UserEntities;
public class JobApplication : EntityBase<int>
{
    public int CustomerId { get; set; }
    public int MajorId { get; set; }
    public string FullName { get; set; }
    public string Certification { get; set; }
    public string Avatar { get; set; }
    public string WorkPlace { get; set; }
    public int ExperienceYear { get; set; }
    public ApplicationStatus Status { get; set; }
    public string? CancelReason { get; set; }

    public virtual User Customer { get; set; } = null!;
    public void Update(
        string FullName,
        string Certification,
        int MajorId,
        string WorkPlace,
        int ExperienceYear)
    {
        this.FullName = FullName;
        this.Certification = Certification;
        this.MajorId = MajorId;
        this.WorkPlace = WorkPlace;
        this.ExperienceYear = ExperienceYear;
    }
}
