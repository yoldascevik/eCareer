using System;
using System.Collections.Generic;
using System.Linq;
using Career.CAP.Serializer;
using Career.EventHub;
using Career.Exceptions;
using DotNetCore.CAP;
using DotNetCore.CAP.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Career.CAP;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCareerCAP(this IServiceCollection services, Action<CapOptions> capOptions)
    {
        services.AddSingleton<ISerializer, CAPJsonSerializer>();
        services.AddCap(capOptions);

        return services;
    }

    public static IServiceCollection RegisterCAPEvents(this IServiceCollection services, params Type[] assemblyPointerTypes)
    {
        Check.NotNull(assemblyPointerTypes, nameof(assemblyPointerTypes));

        services.AddTransient<IEventDispatcher, CAPEventDispatcher>();

        return services;
    }

    public static IServiceCollection RegisterCAPEventHandlers(this IServiceCollection services, params Type[] assemblyPointerTypes)
    {
        Check.NotNull(assemblyPointerTypes, nameof(assemblyPointerTypes));

        foreach (Type assemblyPointerType in assemblyPointerTypes)
        {
            List<Type> eventHandlers = assemblyPointerType.Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(ICapSubscribe).IsAssignableFrom(t))
                .ToList();

            eventHandlers.ForEach(eventHandler => services.AddTransient(eventHandler));
        }

        return services;
    }
}