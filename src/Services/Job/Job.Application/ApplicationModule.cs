using Career.IoC;
using Career.IoC.IoCModule;
using Job.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Job.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.RegisterModule<DomainModule>();
        }
    }
}