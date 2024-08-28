using BHEP.Application.Authentications;
using BHEP.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BHEP.Infrastructure.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
        => services.AddTransient<IJWTTokenService, JWTTokenService>();
}
