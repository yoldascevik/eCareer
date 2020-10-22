using ARConsistency;
using AutoMapper;
using Career.Cache.Redis;
using Career.Exceptions;
using Career.IoC;
using Career.Migration.DataSeeder;
using Career.Mvc.Extensions;
using Career.Mongo;
using Career.Swagger;
using Definition.Application.MappingProfiles;
using Definition.Application.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Definition.Data;
using Definition.Data.DataSeeders;

namespace Definition.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersion();
            services.AddControllers()
                .AddApiResponseConsistency(options =>
                {
                    Configuration.GetSection("ApiConsistency").Bind(options.ResponseOptions);
                    options.ExceptionStatusCodeHandler.RegisterStatusCodedExceptionBaseType<IStatusCodedException>(type=>type.StatusCode);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAutoMapper(typeof(CityMappingProfile));
            services.AddMongoContext<DefinitionContext>();
            services.AddMongo();

            services.AddCareerDistributedRedisCache(options => Configuration.Bind("Redis", options));
            services.AddSwagger();
            
            services.RegisterModule<DefinitionModule>();
            services.RegisterAllTypes<IDataSeeder>(ServiceLifetime.Scoped, typeof(CityDataSeeder));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            
            app.UseRouting();
            app.UseApiResponseConsistency();
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}
