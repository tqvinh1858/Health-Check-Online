namespace BHEP.Domain.Entities.SaleEntities;
public class ServiceTransaction
{
    public int ServiceId { get; set; }
    public int TransactionId { get; set; }

    public virtual CoinTransaction Transaction { get; set; }
    public virtual Service Service { get; set; }
}
