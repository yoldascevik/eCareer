using Career.Domain.DomainEvent;
using Career.IoC.IoCModule;
using Job.Domain.JobAdvertAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Job.Domain
{
    public class DomainModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddScoped<IJobAdvertDomainService, JobAdvertDomainService>();
            services.AddDomainEvents(this.GetType());
        }
    }
}