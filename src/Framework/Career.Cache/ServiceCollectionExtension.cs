using AspectCore;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Cache;

public static class ServiceCollectionExtension
{
    public static IServiceCollection DecorateAllInterfacesUsingAspect(this IServiceCollection services, params Type[] assemblyPointerTypes)
    {
        foreach (Type assemblyPointerType in assemblyPointerTypes)
            services.DecorateAllInterfacesUsingAspect(assemblyPointerType.Assembly);

        return services;
    }
}