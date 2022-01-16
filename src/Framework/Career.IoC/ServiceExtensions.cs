using Career.IoC.IoCModule;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Career.IoC;

public static class ServiceExtensions
{
    public static void RegisterModule(this IServiceCollection services, IModule module)
        => module.Configure(services);

    public static void RegisterModule<TModule>(this IServiceCollection services) where TModule: IModule
        => Activator.CreateInstance<TModule>().Configure(services);

    public static void RegisterModules(this IServiceCollection services, Assembly assembly)
    {
        foreach (Type moduleType in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(IoCModule.Module))))
        {
            if (Activator.CreateInstance(moduleType) is IoCModule.Module module)
                module.Configure(services);
        }
    }
        
    public static void RegisterAllTypes<TService>(
        this IServiceCollection services, 
        ServiceLifetime lifetimeScope,
        params Type[] assemblyMarkerTypes)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
            
        if (assemblyMarkerTypes == null)
            throw new ArgumentNullException(nameof(assemblyMarkerTypes));
            
        var serviceType = typeof(TService);
            
        IEnumerable<Type> implementationTypes = assemblyMarkerTypes
            .Select(t => t.Assembly)
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => serviceType.IsAssignableFrom(type) &&
                           !type.GetTypeInfo().IsInterface &&
                           !type.GetTypeInfo().IsAbstract);
            
        foreach (var implementationType in implementationTypes)
        {
            switch (lifetimeScope)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped(serviceType, implementationType);
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton(serviceType, implementationType);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(serviceType, implementationType);
                    break;
            }
        }
    }
}