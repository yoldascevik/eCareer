using Career.CAP;
using Career.IoC.IoCModule;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Domain;

public class DomainModule: Module
{
    protected override void Load(IServiceCollection services)
    {
        services.RegisterCAPEvents(this.GetType());
        services.RegisterCAPEventHandlers(this.GetType());
    }
}