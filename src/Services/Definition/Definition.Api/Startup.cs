using ARConsistency;
using AutoMapper;
using Career.Cache.Redis;
using Career.Exceptions;
using Career.IoC;
using Career.Migration.DataSeeder;
using Career.Mvc.Extensions;
using Career.Mongo;
using Career.Swagger;
using Definition.Api.Extensions;
using Definition.Application;
using Definition.Application.Location.City;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Definition.Data;
using Definition.Data.DataSeeders.Location;

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
                    Configuration.GetSection("ARConsistency").Bind(options.ResponseOptions);
                    options.ExceptionStatusCodeHandler.RegisterStatusCodedExceptionBaseType<IStatusCodedException>(type=>type.StatusCode);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAutoMapper(typeof(CityMappingProfile));
            services.AddMongoContext<DefinitionContext>();
            services.AddMongo();
            services.AddSwagger();
            services.RegisterModule<DefinitionModule>();
            services.RegisterAllTypes<IDataSeeder>(ServiceLifetime.Scoped, typeof(CityDataSeeder));
            services.AddCareerDistributedRedisCache(options => Configuration.Bind("Redis", options), typeof(ICityService));
            services.AddCareerAuthentication(Configuration);
            services.AddCareerAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseApiResponseConsistency();
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}
