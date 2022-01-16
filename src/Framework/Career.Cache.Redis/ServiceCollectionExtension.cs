using System;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Career.Cache.Redis;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCareerDistributedRedisCache(
        this IServiceCollection services, 
        Action<RedisCacheOptions> options, 
        params Type[] assemblyPointerTypes)
    {
        var redisOptions = new RedisCacheOptions();
        options.Invoke(redisOptions);

        var multiplexer = ConnectionMultiplexer.Connect(redisOptions.Configuration);
        services.AddStackExchangeRedisCache(options);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddSingleton<ICareerDistributedCache, RedisDistributedCache>();
        services.DecorateAllInterfacesUsingAspect(assemblyPointerTypes);
            
        return services;
    }
}