using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Domain.Entities.BlogEntities;
using BHEP.Domain.Entities.PostEntities;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Entities.UserEntities;
public class User : EntityAuditBase<int>
{
    public User()
    {
        Payments = new HashSet<Payment>();
        Codes = new HashSet<Code>();
        GivenRates = new HashSet<UserRate>();
        ReceivedRates = new HashSet<UserRate>();
        ServiceRates = new HashSet<ServiceRate>();
        AppointmentCustomers = new HashSet<Appointment>();
        AppointmentEmployees = new HashSet<Appointment>();
        Schedules = new HashSet<Schedule>();
        Durations = new HashSet<Duration>();
        UserVouchers = new HashSet<UserVoucher>();
        UserCodes = new HashSet<UserCode>();
        ProductRates = new HashSet<ProductRate>();
        CoinTransactions = new HashSet<CoinTransaction>();
        Blogs = new HashSet<Blog>();
        BlogRates = new HashSet<BlogRate>();
        Comments = new HashSet<Comment>();
        CommentLikes = new HashSet<CommentLike>();
        PostLikes = new HashSet<PostLike>();
        Replies = new HashSet<Reply>();
        ReplyLikes = new HashSet<ReplyLike>();
        HealthRecords = new HashSet<HealthRecord>();

        Balance = 0;
    }

    public int RoleId { get; set; }
    public int GeoLocationId { get; set; }
    public int? SpecialistId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    public bool IsActive { get; set; }
    public decimal Balance { get; set; }

    #region Appointment
    public virtual Specialist Specialist { get; set; } = null!;
    public virtual ICollection<Appointment> AppointmentCustomers { get; set; }
    public virtual ICollection<Appointment> AppointmentEmployees { get; set; }

    #endregion

    #region Sale
    public virtual ICollection<Code> Codes { get; set; }
    public virtual ICollection<Duration> Durations { get; set; }
    public virtual ICollection<CoinTransaction> CoinTransactions { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }
    public virtual ICollection<ServiceRate> ServiceRates { get; set; }
    public virtual ICollection<ProductRate> ProductRates { get; set; }

    #endregion

    #region Blog
    public virtual ICollection<Blog> Blogs { get; set; }
    public virtual ICollection<BlogRate> BlogRates { get; set; }
    #endregion

    #region Post
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<CommentLike> CommentLikes { get; set; }
    public virtual ICollection<PostLike> PostLikes { get; set; }
    public virtual ICollection<Reply> Replies { get; set; }
    public virtual ICollection<ReplyLike> ReplyLikes { get; set; }

    #endregion

    #region User
    public virtual GeoLocation GeoLocation { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
    public virtual WorkProfile? Profile { get; set; } = null!;
    public virtual ICollection<UserRate> GivenRates { get; set; }
    public virtual ICollection<UserRate> ReceivedRates { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
    public virtual ICollection<UserCode> UserCodes { get; set; }
    public virtual ICollection<UserVoucher> UserVouchers { get; set; }
    public virtual ICollection<HealthRecord> HealthRecords { get; set; }
    #endregion


    public void Update(
        string fullName,
        string email,
        string phoneNumber,
        Gender gender,
        string? avatar)
    {
        FullName = fullName;
        Email = email;
        Gender = gender;
        Avatar = avatar;
        PhoneNumber = phoneNumber;
        UpdatedDate = DateTime.Now;
    }
}
