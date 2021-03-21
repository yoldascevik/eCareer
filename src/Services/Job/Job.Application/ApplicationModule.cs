using Career.IoC;
using Career.IoC.IoCModule;
using Job.Domain;
using Job.Domain.CandidateAggregate.Repositories;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.TagAggregate.Repositories;
using Job.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Job.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            
            services.RegisterModule<DomainModule>();
        }
    }
}