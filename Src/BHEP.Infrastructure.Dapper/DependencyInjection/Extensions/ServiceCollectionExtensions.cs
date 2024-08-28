using BHEP.Infrastructure.Dapper.Repositories;
using BHEP.Infrastructure.Dapper.Repositories.Interfaces;
using DemoCICD.Infrastructure.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BHEP.Infrastructure.Dapper.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDapper(this IServiceCollection services)
        => services.AddTransient<IUserRepository, UserRepository>()
                    .AddTransient<IRoleRepository, RoleRepository>();
}
