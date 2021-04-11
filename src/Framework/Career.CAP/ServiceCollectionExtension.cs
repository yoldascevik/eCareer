using System;
using System.Collections.Generic;
using System.Linq;
using Career.CAP.DomainEvent;
using Career.CAP.Serializer;
using Career.Domain.DomainEvent;
using Career.Domain.DomainEvent.Dispatcher;
using Career.Exceptions;
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

        public static IServiceCollection AddCAPDomainEvents(this IServiceCollection services, params Type[] assemblyPointerTypes)
        {
            Check.NotNull(assemblyPointerTypes, nameof(assemblyPointerTypes));

            services.AddTransient<IDomainEventDispatcher, CAPDomainEventDispatcher>();
            services.UseDomainEventDispatcherAttribute(assemblyPointerTypes);
            services.AddCAPEventHandlers(assemblyPointerTypes);

            return services;
        }

        public static IServiceCollection AddCAPEventHandlers(this IServiceCollection services, params Type[] assemblyPointerTypes)
        {
            Check.NotNull(assemblyPointerTypes, nameof(assemblyPointerTypes));

            foreach (Type assemblyPointerType in assemblyPointerTypes)
            {
                List<Type> domainEventHandlers = assemblyPointerType.Assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && typeof(ICapSubscribe).IsAssignableFrom(t))
                    .ToList();

                domainEventHandlers.ForEach(eventHandler => services.AddTransient(eventHandler));
            }

            return services;
        }
    }
}