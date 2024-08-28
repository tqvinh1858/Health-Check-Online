using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.SaleEntities;
public class Product : EntityBase<int>
{
    public Product()
    {
        ProductTransactions = new HashSet<ProductTransaction>();
        ProductRates = new HashSet<ProductRate>();
    }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
    public bool SoldOut => Stock <= 0;
    public virtual ICollection<ProductTransaction> ProductTransactions { get; set; }
    public virtual ICollection<ProductRate> ProductRates { get; set; }


    public void Update(string name, string image, string description, float price, int stock)
    {
        Name = name;
        Image = image;
        Description = description;
        Price = price;
        Stock = stock;
    }
}
