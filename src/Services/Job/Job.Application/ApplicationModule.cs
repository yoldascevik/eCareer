using AutoMapper;
using Career.CAP;
using Career.IoC;
using Career.IoC.IoCModule;
using Job.Application.Job;
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
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            
            services.RegisterModule<DomainModule>();
            services.RegisterCAPEventHandlers(this.GetType());
            services.AddAutoMapper(typeof(JobMappingProfile));
        }
    }
}