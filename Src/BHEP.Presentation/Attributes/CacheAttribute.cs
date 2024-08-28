using System.Text;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Infrastructure.Redis.DependencyInjection.Options;
using BHEP.Infrastructure.Redis.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BHEP.Presentation.Attributes;

public class CacheAttribute<TResponse> : Attribute, IAsyncActionFilter
    where TResponse : class
{
    private readonly int timeToLiveSeconds;

    public CacheAttribute(int timeToLiveSeconds = 30)
    {
        this.timeToLiveSeconds = timeToLiveSeconds;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisOptions>();
        if (!cacheConfiguration.Enable)
        {
            await next();
            return;
        }

        try
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IRedisCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = JsonConvert.DeserializeObject<Result<TResponse>>(cacheResponse);
                if (contentResult.IsSuccess)
                    contentResult.Message = "Redis Cache";
                context.Result = new ObjectResult(contentResult);
                return;
            }

            var excusedContent = await next();
            if (excusedContent.Result is ObjectResult objectResult)
                await cacheService.SetCacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(timeToLiveSeconds));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private static string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var keyBuilder = new StringBuilder();
        keyBuilder.Append($"{request.Path}");
        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($"|{key}-{value}");
        }
        return keyBuilder.ToString().ToLower();
    }
}


