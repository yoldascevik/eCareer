using Career.Consul;
using Definition.Api.Constants;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Definition.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCareerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["ServiceUrls:Identity"];
                    options.ApiName = "definitionapi";
                    options.ApiSecret = "apisecret";
                    options.RequireHttpsMetadata = false;
                });

            return services;
        }
        
        public static IServiceCollection AddCareerAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.Manage, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole("Admin");
                });   
            });

            return services;
        }
        
        public static IServiceCollection AddCareerConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
            return services;
        }
    }
}