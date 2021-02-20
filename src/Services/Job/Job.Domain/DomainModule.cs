using Career.Domain;
using Career.IoC.IoCModule;
using Microsoft.Extensions.DependencyInjection;

namespace Job.Domain
{
    public class DomainModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddDomainEvents(this.GetType());
        }
    }
}