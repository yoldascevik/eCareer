using ARConsistency;
using Career.CAP;
using Career.Exceptions;
using Career.IoC;
using Career.Mongo;
using Career.Mvc.Extensions;
using Career.Shared.Timing;
using Career.Swagger;
using CurriculumVitae.Api.Extensions;
using CurriculumVitae.Application;
using CurriculumVitae.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurriculumVitae.Api
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
            Clock.Provider = ClockProviders.Utc;

            services.AddApiVersion();
            services.AddControllers()
                .AddApiResponseConsistency(options =>
                {
                    Configuration.GetSection("ARConsistency").Bind(options.ResponseOptions);
                    options.ExceptionStatusCodeHandler.RegisterStatusCodedExceptionBaseType<IStatusCodedException>(type => type.StatusCode);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            services.AddMongoContext<CvDbContext>();
            services.AddMongo();
            CvDbContext.Configure();
            
            services.RegisterModule<ApplicationModule>();
            services.AddSwagger();
            
            services.AddCareerCAP(capOptions =>
            {
                capOptions.UseRabbitMQ(opt => // Transport
                {
                    opt.Password = Configuration["rabbitMQSettings:password"];
                    opt.UserName = Configuration["rabbitMQSettings:username"];
                    opt.HostName = Configuration["rabbitMQSettings:host"];
                    opt.Port = int.Parse(Configuration["rabbitMQSettings:port"]);
                });
                capOptions.UseMongoDB(opt => // Persistence
                {
                    opt.DatabaseConnection = Configuration["mongo:connectionString"];
                    opt.DatabaseName = Configuration["mongo:database"] +  "EventHistories";
                    opt.PublishedCollection = "publishedEvents";
                    opt.ReceivedCollection = "receivedEvents";
                });
                capOptions.UseDashboard();
                capOptions.FailedRetryCount = 3;
                capOptions.FailedRetryInterval = 60;
            });

            services.AddCareerConsul(Configuration);
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