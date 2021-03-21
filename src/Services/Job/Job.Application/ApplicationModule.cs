using Career.IoC;
using Career.IoC.IoCModule;
using Job.Domain;
using Job.Domain.JobAdvertAggregate.Repositories;
using Job.Domain.JobApplicationAggregate.Repositories;
using Job.Domain.TagAggregate.Repositories;
using Job.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Job.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddScoped<IJobAdvertRepository, JobAdvertRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            
            services.RegisterModule<DomainModule>();
        }
    }
}