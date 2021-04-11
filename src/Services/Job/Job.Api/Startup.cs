using ARConsistency;
using Career.CAP;
using Career.Exceptions;
using Career.IoC;
using Career.MediatR;
using Career.Mongo;
using Career.Mvc.Extensions;
using Career.Shared.Timing;
using Career.Swagger;
using Job.Application;
using Job.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Savorboard.CAP.InMemoryMessageQueue;

namespace Job.Api
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
                    Configuration.GetSection("ApiConsistency").Bind(options.ResponseOptions);
                    options.ExceptionStatusCodeHandler.RegisterStatusCodedExceptionBaseType<IStatusCodedException>(type=>type.StatusCode);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            services.AddMongoContext<JobDbContext>();
            services.AddMongo();
            
            JobDbContext.Configure();
            
            services.RegisterModule<ApplicationModule>();
            services.AddMediatRWithFluentValidation(typeof(ApplicationModule));
            services.AddSwagger();
            
            services.AddCareerCAP(capOptions =>
            {
                capOptions.UseInMemoryMessageQueue(); // Transport
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