namespace BHEP.Infrastructure.Redis.Service.IService;
public interface IRedisCacheService
{
    Task<T> GetAsync<T>(string key);
    Task<object> GetAsync(string key);
    Task SetAsync(string key, object value);
    Task<bool> SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
    Task<string> GetCacheResponseAsync(string cacheKey);
    Task RemoveCacheResponseAsync(string partern);
}
