using System;
using AspectCore;
using Career.Domain.DomainEvent.Dispatcher;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Domain.DomainEvent
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection services, params Type[] assemblyPointerTypes)
        {
            services.AddMediatR(assemblyPointerTypes);
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

            foreach (Type assemblyPointerType in assemblyPointerTypes)
                services.DecorateAllInterfacesUsingAspect(assemblyPointerType.Assembly);
            
            return services;
        }
    }
}