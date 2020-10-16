using Microsoft.Extensions.DependencyInjection;

namespace Career.IoC.IoCModule
{
    public interface IModule
    {
        void Configure(IServiceCollection services);
    }
}
