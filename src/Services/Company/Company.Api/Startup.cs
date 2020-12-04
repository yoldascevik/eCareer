using ARConsistency;
using Career.Exceptions;
using Career.IoC;
using Career.MediatR;
using Career.Mvc.Extensions;
using Career.Swagger;
using Company.Application.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Company.Api
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
            
            services.AddMediatRWithFluentValidation(typeof(ApplicationModule));
            services.RegisterModule<ApplicationModule>();
            services.AddSwagger();
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