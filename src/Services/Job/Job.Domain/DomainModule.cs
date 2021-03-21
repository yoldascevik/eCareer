using Career.Domain.DomainEvent;
using Career.IoC.IoCModule;
using Job.Domain.JobAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Job.Domain
{
    public class DomainModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddScoped<IJobDomainService, JobDomainService>();
            services.AddDomainEvents(this.GetType());
        }
    }
}