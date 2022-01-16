using Career.CAP;
using Career.IoC.IoCModule;
using Microsoft.Extensions.DependencyInjection;

namespace CurriculumVitae.Core;

public class CoreModule : Module
{
    protected override void Load(IServiceCollection services)
    {
        services.RegisterCAPEvents(this.GetType());
    }
}