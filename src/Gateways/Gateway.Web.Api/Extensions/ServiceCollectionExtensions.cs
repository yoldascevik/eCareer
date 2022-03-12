using Career.Consul;

namespace Gateway.Web.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCareerConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
        return services;
    }
}