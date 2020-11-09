using Career.Http.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Http
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCaareerHttpClient(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient();
            services.AddHttpClient<ICareerHttpClient, CareerHttpClient>()
                .AddHttpMessageHandler<HttpClientRequestIdDelegatingHandler>();

            return services;
        }
    }
}