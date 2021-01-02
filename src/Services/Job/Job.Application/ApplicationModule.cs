using Career.Configuration;
using Career.EntityFramework;
using Career.IoC.IoCModule;
using Job.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Job.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();

            services.AddDbContext<JobDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("JobDatabase")));
            services.AddUnitOfWork<JobDbContext>();
        }
    }
}