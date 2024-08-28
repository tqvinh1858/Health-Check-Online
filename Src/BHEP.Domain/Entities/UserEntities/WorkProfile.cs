using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.UserEntities;
public class WorkProfile : EntityBase<int>
{
    public int UserId { get; set; }
    public int MajorId { get; set; }
    public string WorkPlace { get; set; }
    public string Certificate { get; set; }
    public int ExperienceYear { get; set; }
    public decimal Price { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Major Major { get; set; } = null!;

    public void Update(
        string WorkPlace,
        int MajorId,
        string Certificate,
        int ExperienceYear,
        decimal Price)
    {
        this.WorkPlace = WorkPlace;
        this.MajorId = MajorId;
        this.Certificate = Certificate;
        this.ExperienceYear = ExperienceYear;
        this.Price = Price;
    }
}
