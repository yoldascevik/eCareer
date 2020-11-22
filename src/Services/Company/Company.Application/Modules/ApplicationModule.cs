using Career.Configuration;
using Career.IoC.IoCModule;
using Career.Shared.OS;
using Definition.HttpClient;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application.Modules
{
    public class ApplicationModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();
            var definitionApiEndPoint = configuration.GetSection("ApiEndpoints:DefinitionApi").Get<ApiEndpointOptions>();
            
            services.AddDateTimeProvider();
            services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);
            services.AddDefinitionApiHttpClient(definitionApiEndPoint);
        }
    }
}