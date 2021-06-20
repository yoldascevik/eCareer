using Career.CAP;
using Career.IoC;
using Career.IoC.IoCModule;
using Career.MediatR;
using Career.Shared.Generators;
using CurriculumVitae.Core;
using CurriculumVitae.Core.Repositories;
using CurriculumVitae.Infrastructure.Repositories;
using CurriculumVitae.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CurriculumVitae.Application
{
    public class ApplicationModule: IModule
    {
        public void Configure(IServiceCollection services)
        {
            services.AddScoped<ICVRepository, CVRepository>();
            services.AddScoped<IDisabilityTypeRepository, DisabilityTypeRepository>();
            services.AddScoped<ISocialProfileTypeRepository, SocialProfileTypeRepository>();
            
            services.AddSingleton<IStringIdGenerator, StringObjectIdGenerator>();

            services.AddMediatRWithFluentValidation(this.GetType());
            services.AddAutoMapper(this.GetType());
            
            services.RegisterModule<CoreModule>();
            services.RegisterCAPEventHandlers(this.GetType());
        }
    }
}