using BHEP.Infrastructure.BlobStorage.DependencyInjection.Options;
using BHEP.Infrastructure.BlobStorage.Repositories;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BHEP.Infrastructure.BlobStorage.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructureBlobStorage(this IServiceCollection services)
        => services.AddTransient(typeof(IBlobStorageService), typeof(BlobStorageService));
    public static OptionsBuilder<BlobStorageOptions> ConfigureBlobStorageOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<BlobStorageOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
