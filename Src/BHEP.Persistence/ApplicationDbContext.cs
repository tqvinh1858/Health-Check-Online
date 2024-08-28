using BHEP.Domain.Entities;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Domain.Entities.BlogEntities;
using BHEP.Domain.Entities.PostEntities;
using BHEP.Domain.Entities.SaleEntities;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence;
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public ApplicationDbContext() : base()
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    #region AppointmentEntities
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<AppointmentSymptom> AppointmentSymptom { get; set; }
    public DbSet<Specialist> Specialist { get; set; }
    public DbSet<SpecialistSymptom> SpecialistSymptom { get; set; }
    public DbSet<Symptom> Symptom { get; set; }
    #endregion

    #region BlogEntites
    public DbSet<Blog> Blog { get; set; }
    public DbSet<BlogPhoto> BlogPhoto { get; set; }
    public DbSet<BlogRate> BlogRate { get; set; }
    public DbSet<BlogTopic> BlogTopic { get; set; }
    public DbSet<Topic> Topic { get; set; }
    #endregion

    #region SaleEntities
    public DbSet<Code> Code { get; set; }
    public DbSet<CoinTransaction> CoinTransaction { get; set; }
    public DbSet<Duration> Duration { get; set; }
    public DbSet<Device> Device { get; set; }
    public DbSet<ProductTransaction> ProductTransaction { get; set; }
    public DbSet<ServiceTransaction> ServiceTransaction { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DbSet<VoucherTransaction> VoucherTransaction { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductRate> ProductRate { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<ServiceRate> ServiceRate { get; set; }
    public DbSet<Voucher> Voucher { get; set; }
    public DbSet<CoinTransaction> CoinTransactions { get; set; }
    #endregion

    #region UserEntities
    public DbSet<GeoLocation> GeoLocation { get; set; }
    public DbSet<JobApplication> JobApplication { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Schedule> Schedule { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserCode> UserCode { get; set; }
    public DbSet<UserRate> UserRate { get; set; }
    public DbSet<UserVoucher> UserVoucher { get; set; }
    public DbSet<WorkProfile> WorkProfile { get; set; }
    public DbSet<HealthRecord> HealthRecord { get; set; }
    #endregion

    #region PostEntities
    public DbSet<Post> Post { get; set; }
    public DbSet<PostSpecialist> PostSpecialist { get; set; }
    public DbSet<PostLike> PostLike { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<CommentLike> CommentLike { get; set; }
    public DbSet<Reply> Reply { get; set; }
    public DbSet<ReplyLike> ReplyLike { get; set; }

    #endregion

    public DbSet<Major> Major { get; set; }
}
