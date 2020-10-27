﻿using System;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Cache.Redis
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCareerDistributedRedisCache(
            this IServiceCollection services, 
            Action<RedisCacheOptions> options, 
            params Type[] assemblyPointerTypes)
        {
            services.AddDistributedRedisCache(options);
            services.AddSingleton<ICareerDistributedCache, RedisDistributedCache>();
            services.DecorateAllInterfacesUsingAspect(assemblyPointerTypes);

            return services;
        }
    }
}