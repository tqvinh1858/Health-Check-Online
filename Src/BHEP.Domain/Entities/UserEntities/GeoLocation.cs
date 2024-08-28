using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.UserEntities;
public class GeoLocation : EntityBase<int>
{
    public string Latitude { get; set; }
    public string Longitude { get; set; }

    public virtual User User { get; set; } = null!;

    public void Update(string latitude, string longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
