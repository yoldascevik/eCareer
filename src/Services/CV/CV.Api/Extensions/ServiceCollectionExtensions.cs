﻿using ARConsistency;
using Career.Consul;
using Career.CAP;
using Career.Exceptions;
using CurriculumVitae.Api.Constants;
using IdentityServer4.AccessTokenValidation;

namespace CurriculumVitae.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCareerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["ServiceUrls:Identity"];
                options.ApiName = "cvapi";
                options.ApiSecret = "apisecret";
                options.RequireHttpsMetadata = false;
            });

        return services;
    }
        
    public static IServiceCollection AddCareerAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.ManageCv, policyBuilder =>
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