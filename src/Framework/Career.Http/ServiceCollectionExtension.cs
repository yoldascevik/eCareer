using Career.Http.HttpClient;
using Career.Http.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Http
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCareerHttpClient(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<ICareerHttpClient, CareerHttpClient>()
                .AddHttpMessageHandler<HttpClientRequestIdDelegatingHandler>();

            return services;
        }
    }
}