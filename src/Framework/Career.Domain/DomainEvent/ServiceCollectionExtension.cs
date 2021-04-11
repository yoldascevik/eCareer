using System;
using AspectCore;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Domain.DomainEvent
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection UseDomainEventDispatcherAttribute(this IServiceCollection services, params Type[] assemblyPointerTypes)
        {
            foreach (Type assemblyPointerType in assemblyPointerTypes)
                services.DecorateAllInterfacesUsingAspect(assemblyPointerType.Assembly);
            
            return services;
        }
    }
}