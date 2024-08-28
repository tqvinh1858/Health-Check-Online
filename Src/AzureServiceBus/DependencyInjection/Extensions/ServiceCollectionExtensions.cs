using BHEP.Infrastructure.ServiceBus.DependencyInjection.Options;
using BHEP.Infrastructure.ServiceBus.Services;
using BHEP.Infrastructure.ServiceBus.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BHEP.Infrastructure.ServiceBus.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructureServiceBus(this IServiceCollection services)
        => services.AddTransient(typeof(IMessagePublisher), typeof(MessagePublisher))
        //                    .AddHostedService<CreateCoinTransactionConsumer>()
        //                    .AddHostedService<CreateCoinTransactionDLQ>()
        ;
    public static OptionsBuilder<ServiceBusOptions> ConfigureServiceBusOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<ServiceBusOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
