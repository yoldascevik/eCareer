using System;
using Career.Domain.DomainEvent.Dispatcher;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Domain
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection services, params Type[] assemblyPointerTypes)
        {
            services.AddMediatR(assemblyPointerTypes);
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

            return services;
        }
    }
}