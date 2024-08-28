namespace BHEP.Domain.Entities.SaleEntities;
public class ProductTransaction

{
    public int ProductId { get; set; }
    public int TransactionId { get; set; }
    public int Quantity { get; set; }

    public virtual Product Product { get; set; }
    public virtual CoinTransaction Transaction { get; set; }
}
