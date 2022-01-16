using ARConsistency;
using Career.Configuration;
using Career.IoC;
using Career.MediatR;
using Career.Mongo;
using Career.Mvc.Extensions;
using Career.Shared.Timing;
using Career.Swagger;
using Job.Api.Extensions;
using Job.Application;
using Job.Infrastructure;
using Logging;
using Serilog;

var configuration = ConfigurationHelper.GetConfiguration();
Log.Logger = CareerSerilogLoggerFactory.CreateSerilogLogger(configuration);

try
{
    Clock.Provider = ClockProviders.Utc;
    
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;

    services.AddApiVersion();
    services.AddControllersWithARConsistency(configuration);
    services.AddMongoContext<JobDbContext>();
    services.AddMongo();
    services.RegisterModule<ApplicationModule>();
    services.AddMediatRWithFluentValidation(typeof(ApplicationModule));
    services.AddSwagger();
    services.AddCareerCAP(configuration);
    services.AddCareerConsul(configuration);
    services.AddCareerAuthentication(configuration);
    services.AddCareerAuthorization();

    JobDbContext.Configure();
    builder.Host.UseSerilog();
    
    var app = builder.Build();
    
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