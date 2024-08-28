using BHEP.Infrastructure.Redis.DependencyInjection.Options;
using BHEP.Infrastructure.Redis.Service;
using BHEP.Infrastructure.Redis.Service.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BHEP.Infrastructure.Redis.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigRedis(this IServiceCollection services, IConfigurationSection section)
    {
        var redisOptions = new RedisOptions();
        section.Bind(redisOptions);
        services.AddSingleton(redisOptions);

        if (!redisOptions.Enable)
            return services;
        var connectionMultiplexer = ConnectionMultiplexer.Connect(redisOptions.ConnectionString);

        services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
        services.AddSingleton<IDatabase>(connectionMultiplexer.GetDatabase());
        services.AddStackExchangeRedisCache(options => options.Configuration = redisOptions.ConnectionString);
        services.AddSingleton<IRedisCacheService, RedisCacheService>();
        return services;
    }
}
