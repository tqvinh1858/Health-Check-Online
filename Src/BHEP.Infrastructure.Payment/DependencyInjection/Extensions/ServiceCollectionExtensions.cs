using BHEP.Infrastructure.VnPay.DependencyInjection.Options.Config;
using BHEP.Infrastructure.VnPay.Respository;
using BHEP.Infrastructure.VnPay.Respository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BHEP.Infrastructure.VnPay.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructureVnPay(this IServiceCollection services)
    {
        services.AddTransient<IVnPayRepository, VnPayRepository>();
        return services;
    }

    public static OptionsBuilder<VnPayConfig> ConfigureVnPayOptions(this IServiceCollection services, IConfigurationSection section)
    {
        return services.AddOptions<VnPayConfig>()
                       .Bind(section)
                       .ValidateOnStart();
    }

}
