using ARConsistency;
using Career.Consul;
using Career.Exceptions;
using Career.CAP;
using IdentityServer4.AccessTokenValidation;
using Job.Api.Constants;

namespace Job.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCareerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["ServiceUrls:Identity"];
                options.ApiName = "jobapi";
                options.ApiSecret = "apisecret";
                options.RequireHttpsMetadata = false;
            });

        return services;
    }
        
    public static IServiceCollection AddCareerAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.ManageJob, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireRole(AuthorizationRoles.JobAdmin.ToString());
            });  
            options.AddPolicy(AuthorizationPolicies.PublishJob, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireRole(AuthorizationRoles.JobPublisher.ToString());
            });
            options.AddPolicy(AuthorizationPolicies.Candidate, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireRole(AuthorizationRoles.Candidate.ToString());
            });
        });

        return services;
    }
        
    public static IServiceCollection AddCareerConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
        return services;
    }
    
    public static IServiceCollection AddCareerCAP(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCareerCAP(capOptions =>
        {
            capOptions.UseRabbitMQ(opt => // Transport
            {
                opt.UserName = configuration["RabbitMQSettings:Username"];
                opt.Password = configuration["RabbitMQSettings:Password"];
                opt.HostName = configuration["RabbitMQSettings:Host"];
                opt.Port = int.Parse(configuration["RabbitMQSettings:Port"]);
            });
            capOptions.UseMongoDB(opt => // Persistence
            {
                opt.DatabaseConnection = configuration["MongoDb:ConnectionString"];
                opt.DatabaseName = configuration["MongoDb:Database"];
                opt.PublishedCollection = "publishedEvents";
                opt.ReceivedCollection = "receivedEvents";
            });
            capOptions.UseDashboard();
            capOptions.FailedRetryCount = 3;
            capOptions.FailedRetryInterval = 60;
        });
        
        return services;
    }
    
    public static IServiceCollection AddControllersWithARConsistency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddApiResponseConsistency(options =>
            {
                configuration.GetSection("ARConsistency").Bind(options.ResponseOptions);
                options.ExceptionStatusCodeHandler.RegisterStatusCodedExceptionBaseType<IStatusCodedException>(type => type.StatusCode);
            });
        
        return services;
    }
}