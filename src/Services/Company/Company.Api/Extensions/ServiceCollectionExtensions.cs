using ARConsistency;
using Career.CAP;
using Career.Consul;
using Career.Exceptions;
using Company.Api.Constants;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCareerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["ServiceUrls:Identity"];
                options.ApiName = "companyapi";
                options.ApiSecret = "apisecret";
                options.RequireHttpsMetadata = false;
            });

        return services;
    }
        
    public static IServiceCollection AddCareerAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.ManageCompany, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireRole(AuthorizationRoles.CompanyAdmin.ToString());
            });  
            options.AddPolicy(AuthorizationPolicies.FollowCompany, policyBuilder =>
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
                opt.Password = configuration["rabbitMQSettings:password"];
                opt.UserName = configuration["rabbitMQSettings:username"];
                opt.HostName = configuration["rabbitMQSettings:host"];
                opt.Port = int.Parse(configuration["rabbitMQSettings:port"]);
            });
            capOptions.UseMongoDB(opt => // Persistence
            {
                opt.DatabaseConnection = configuration["EventBusStorage:connectionString"];
                opt.DatabaseName = configuration["EventBusStorage:database"];
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