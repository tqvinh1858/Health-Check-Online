using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.DependencyInjection.Options;
using BHEP.Persistence.Repositories;
using BHEP.Persistence.Repositories.AppointmentRepo;
using BHEP.Persistence.Repositories.BlogRepo;
using BHEP.Persistence.Repositories.Interface;
using BHEP.Persistence.Repositories.PostRepo;
using BHEP.Persistence.Repositories.SaleRepo;
using BHEP.Persistence.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BHEP.Persistence.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptions<SqlServerRetryOptions>>();

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("ConnectionStrings"),
                sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder.ExecutionStrategy(
                                dependencies => new SqlServerRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: options.Value.MaxRetryCount,
                                    maxRetryDelay: options.Value.MaxRetryDelay,
                                    errorNumbersToAdd: options.Value.ErrorNumbersToAdd))
                            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
        });
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        => services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork))
            .AddTransient<IAppointmentRepository, AppointmentRepository>()
            .AddTransient<IAppointmentSymptomRepository, AppointmentSymptomRepository>()
            .AddTransient<ISpecialistSymptomRepository, SpecialistSymptomRepository>()
            .AddTransient<ICodeRepository, CodeRepository>()
            .AddScoped<ICoinTransactionRepository, CoinTransactionRepository>()
            .AddTransient<IDurationRepository, DurationRepository>()
            .AddTransient<IDeviceRepository, DeviceRepository>()
            .AddScoped<IGeoLocationRepository, GeoLocationRepository>()
            .AddScoped<IJobApplicationRepository, JobApplicationRepository>()
            .AddScoped<IPaymentRepository, PaymentRepository>()
            .AddTransient<IPaymentVoucherRepository, PaymentVoucherRepository>()
            .AddScoped<IProductRateRepository, ProductRateRepository>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IScheduleRepository, ScheduleRepository>()
            .AddScoped<IServiceRateRepository, ServiceRateRepository>()
            .AddScoped<IServiceRepository, ServiceRepository>()
            .AddTransient<ISpecialistRepository, SpecialistRepository>()
            .AddTransient<ISymptomRepository, SymptomRepository>()
            .AddScoped<IUserCodeRepository, UserCodeRepository>()
            .AddScoped<IUserRateRepository, UserRateRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserVoucherRepository, UserVoucherRepository>()
            .AddTransient<IVoucherRepository, VoucherRepository>()
            .AddTransient<IMajorRepository, MajorRepository>()
            .AddTransient<IWorkProfileRepository, WorkProfileRepository>()
            .AddTransient<ITopicRepository, TopicRepository>()
            .AddTransient<IBlogRepository, BlogRepository>()
            .AddTransient<IBlogPhotoRepository, BlogPhotoRepository>()
            .AddTransient<IBlogRateRepository, BlogRateRepository>()
            .AddTransient<IBlogTopicRepository, BlogTopicRepository>()
            .AddTransient<IPostRepository, PostRepository>()
            .AddTransient<IPostLikeRepository, PostLikeRepository>()
            .AddTransient<IPostSpecialistRepository, PostSpecialistRepository>()
            .AddScoped<IHealthRecordRepository, HealthRecordRepository>()
            .AddScoped<ICommentRepository, CommentRepository>()
            .AddScoped<ICommentLikeRepository, CommentLikeRepository>()
            .AddScoped<IReplyRepository, ReplyRepository>()
            .AddScoped<IReplyLikeRepository, ReplyLikeRepository>()
            .AddScoped<IDeletionRequestRepository, DeletionRequestRepository>()
            ;

    public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<SqlServerRetryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
