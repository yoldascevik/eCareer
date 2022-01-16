using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Career.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCareerHttpClient(this IServiceCollection services)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        return services;
    }

    #region AddHttpClient With Retry Policy

    public static IHttpClientBuilder AddHttpClientWithRetryPolicy<TClient, TImplementation>(
        this IServiceCollection services, Action<System.Net.Http.HttpClient> configureClient, 
        int retryCount = 3, int waitingMilisecond = 300)
        where TClient : class
        where TImplementation : class, TClient
    {
        return services.AddHttpClient<TClient, TImplementation>(configureClient)
            .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(retryCount, _ => TimeSpan.FromMilliseconds(waitingMilisecond)));
    }
        
    public static IHttpClientBuilder AddHttpClientWithRetryPolicy<TClient, TImplementation>(
        this IServiceCollection services, Action<System.Net.Http.HttpClient> configureClient,
        Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> configurePolicy)
        where TClient : class
        where TImplementation : class, TClient
    {
        return services.AddHttpClient<TClient, TImplementation>(configureClient).AddTransientHttpErrorPolicy(configurePolicy);
    }

    public static IHttpClientBuilder AddHttpClientWithRetryPolicy<TClient, TImplementation>(
        this IServiceCollection services, int retryCount = 3, int waitingMilisecond = 300)
        where TClient : class
        where TImplementation : class, TClient
    {
        return services.AddHttpClient<TClient, TImplementation>()
            .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(retryCount, _ => TimeSpan.FromMilliseconds(waitingMilisecond)));
    }
        
    public static IHttpClientBuilder AddHttpClientWithRetryPolicy<TClient, TImplementation>(this IServiceCollection services,
        Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> configurePolicy)
        where TClient : class
        where TImplementation : class, TClient
    {
        return services.AddHttpClient<TClient, TImplementation>().AddTransientHttpErrorPolicy(configurePolicy);
    }

    #endregion
}