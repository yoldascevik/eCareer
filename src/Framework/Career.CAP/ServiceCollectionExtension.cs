using System;
using System.Collections.Generic;
using System.Linq;
using Career.CAP.DomainEvent;
using Career.CAP.Serializer;
using Career.Domain.DomainEvent;
using Career.Domain.DomainEvent.Dispatcher;
using DotNetCore.CAP;
using DotNetCore.CAP.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Career.CAP
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCareerCAP(this IServiceCollection services, Action<CapOptions> capOptions)
        {
            services.AddSingleton<ISerializer, CAPJsonSerializer>();
            services.AddCap(capOptions);

            return services;
        }

        public static IServiceCollection AddDomainEvents(this IServiceCollection services, Type assemblyPointerType)
        {
            services.AddTransient<IDomainEventDispatcher, CAPDomainEventDispatcher>();
            services.UseDomainEventDispatcherAttribute(assemblyPointerType);

            List<Type> domainEventHandlers = assemblyPointerType.Assembly.GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && typeof(IDomainEventHandler).IsAssignableFrom(t)
                            && typeof(ICapSubscribe).IsAssignableFrom(t))
                .ToList();

            domainEventHandlers.ForEach(eventHandler => services.AddTransient(eventHandler));

            return services;
        }
    }
}