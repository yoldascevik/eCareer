using ARConsistency;
using Career.Configuration;
using Career.EntityFramework;
using Career.IoC;
using Career.MediatR;
using Career.Mvc.Extensions;
using Career.Shared.Timing;
using Career.Swagger;
using Company.Api.Extensions;
using Company.Application;
using Company.Infrastructure;
using Logging;
using Serilog;

var configuration = ConfigurationHelper.GetConfiguration();
Log.Logger = CareerSerilogLoggerFactory.CreateSerilogLogger(configuration);

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    
    Clock.Provider = ClockProviders.Utc;
    
    services.AddApiVersion();
    services.AddControllersWithARConsistency(configuration);
    services.RegisterModule<ApplicationModule>();
    services.AddMediatRWithFluentValidation(typeof(ApplicationModule));
    services.AddSwagger();
    services.AddCareerCAP(configuration);
    services.AddCareerConsul(configuration);
    services.AddCareerAuthentication(configuration);
    services.AddCareerAuthorization();
    
    builder.Host.UseSerilog();
    
    var app = builder.Build();
    app.MigrateEntityFrameworkDatabase<CompanyDbContext>();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseApiResponseConsistency();
    app.MapControllers();
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly");
}
finally
{
    Log.CloseAndFlush();
}