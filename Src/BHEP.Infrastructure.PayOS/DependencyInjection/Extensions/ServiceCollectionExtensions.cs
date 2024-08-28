using BHEP.Infrastructure.PayOS.DependencyInjection.Options;
using BHEP.Infrastructure.PayOS.Repository;
using BHEP.Infrastructure.PayOS.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BHEP.Infrastructure.PayOS.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructurePayOS(this IServiceCollection services)
         => services.AddTransient<IPayOSRepository, PayOSRepository>();

    public static OptionsBuilder<PayOSOptions> ConfigurePayOSOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<PayOSOptions>()
                       .Bind(section)
                       .ValidateOnStart();


}
