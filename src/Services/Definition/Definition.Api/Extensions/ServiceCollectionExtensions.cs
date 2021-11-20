using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Definition.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCareerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            IdentityModelEventSource.ShowPII = true;
            
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = configuration["ServiceUrls:Identity"];
                    opt.ApiName = "definition";
                    opt.ApiSecret = "apisecret";
                    opt.RequireHttpsMetadata = false;
                });

            return services;
        }
    }
}