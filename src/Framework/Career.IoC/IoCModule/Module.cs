using Microsoft.Extensions.DependencyInjection;

namespace Career.IoC.IoCModule
{
    public abstract class Module : IModule
    {
        public void Configure(IServiceCollection services)
        {
            Load(services);
        }

        protected abstract void Load(IServiceCollection services);
    }
}
