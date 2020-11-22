using Career.IoC.IoCModule;
using Career.Shared.OS;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application.Modules
{
    public class ApplicationModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddDateTimeProvider();
        }
    }
}