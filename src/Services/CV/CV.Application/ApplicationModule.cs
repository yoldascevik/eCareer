using Career.IoC.IoCModule;
using Career.MediatR;
using CurriculumVitae.Core.Repositories;
using CurriculumVitae.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CurriculumVitae.Application
{
    public class ApplicationModule: IModule
    {
        public void Configure(IServiceCollection services)
        {
            services.AddScoped<ICVRepository, CVRepository>();
            
            services.AddMediatRWithFluentValidation(this.GetType());
            services.AddAutoMapper(this.GetType());
        }
    }
}