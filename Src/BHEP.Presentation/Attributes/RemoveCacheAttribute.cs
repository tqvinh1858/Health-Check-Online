using BHEP.Infrastructure.Redis.DependencyInjection.Options;
using BHEP.Infrastructure.Redis.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BHEP.Presentation.Attributes;
public class RemoveCacheAttribute : Attribute, IAsyncActionFilter
{
    private readonly string cacheKey;
    public RemoveCacheAttribute(string cacheKey)
    {
        this.cacheKey = cacheKey;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisOptions>();
        if (!cacheConfiguration.Enable)
        {
            await next();
            return;
        }

        var result = await next();
        if (result.Result is OkObjectResult)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IRedisCacheService>();
            await cacheService.RemoveCacheResponseAsync(cacheKey);
        }
    }
}
