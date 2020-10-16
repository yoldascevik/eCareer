using System;
using System.IO;
using System.Reflection;
using Career.Swagger.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Career.Swagger
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            IConfiguration configuration = services
                .BuildServiceProvider()
                .GetService<IConfiguration>();

            var swaggerOptions = ConfigurationHelper.GetSwaggerOptions(configuration);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo
                {
                    Title = swaggerOptions.Title, 
                    Description = swaggerOptions.Description, 
                    Version = swaggerOptions.Version
                });

                if (swaggerOptions.IncludeXmlComments)
                {
                    var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        options.IncludeXmlComments(xmlPath);   
                    }
                }
            });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            IConfiguration configuration = app.ApplicationServices.GetService<IConfiguration>();
            var swaggerOptions = ConfigurationHelper.GetSwaggerOptions(configuration);
            
            app.UseSwagger(options => { options.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
                options.RoutePrefix = swaggerOptions.RoutePrefix;
            });

            return app;
        }
    }
}