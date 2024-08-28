namespace BHEP.Infrastructure.BlobStorage.DependencyInjection.Options;
public class BlobStorageOptions
{
    public string BlobUrl { get; set; }
    public string ResourceGroup { get; set; }
    public string Container { get; set; }
    public string Key { get; set; }
}
