using BHEP.Contract.Constants;

namespace BHEP.Domain.Abstractions.EntityBase;
public interface IDuration
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsExpired => EndDate < TimeZones.SoutheastAsia;
}
