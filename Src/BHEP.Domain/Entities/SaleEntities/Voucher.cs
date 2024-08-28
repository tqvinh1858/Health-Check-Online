using System.ComponentModel.DataAnnotations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class Voucher : EntityDurationBase<int>
{
    public Voucher()
    {
        UserVouchers = new HashSet<UserVoucher>();
        VoucherTransactions = new HashSet<VoucherTransaction>();
    }

    public string Name { get; set; }
    public string Code { get; set; }

    public float Discount { get; set; }
    [Range(1, int.MaxValue)]
    public int Stock { get; set; }
    public bool IsExpired => EndDate < DateTime.UtcNow;
    public bool OutOfStock => Stock <= 0;
    public virtual ICollection<UserVoucher> UserVouchers { get; set; }
    public virtual ICollection<VoucherTransaction> VoucherTransactions { get; set; }


    public void Update(string Name, string Code, float Discount, int Stock, DateTime StartDate, DateTime EndDate)
    {
        this.Name = Name;
        this.Code = Code;
        this.Discount = Discount;
        this.Stock = Stock;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }

}
