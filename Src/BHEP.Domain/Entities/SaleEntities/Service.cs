using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.SaleEntities;
public class Service : EntityBase<int>
{
    public Service()
    {
        ServiceRates = new HashSet<ServiceRate>();
        Durations = new HashSet<Duration>();
        ServiceTransactions = new HashSet<ServiceTransaction>();
    }
    public string Name { get; set; }
    public string Image { get; set; }
    public ServiceType Type { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public TimeExpired Duration { get; set; }

    public virtual ICollection<ServiceRate> ServiceRates { get; set; }
    public virtual ICollection<Duration> Durations { get; set; }
    public virtual ICollection<ServiceTransaction> ServiceTransactions { get; set; }

    public void Update(string name, string image, ServiceType Type, string description, float price, TimeExpired duration)
    {
        Name = name;
        Image = image;
        this.Type = Type;
        Description = description;
        Price = price;
        Duration = duration;
    }
}

