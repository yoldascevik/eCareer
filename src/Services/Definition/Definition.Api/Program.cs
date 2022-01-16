using ARConsistency;
using Career.Cache.Redis;
using Career.Configuration;
using Career.IoC;
using Career.Migration;
using Career.Migration.DataSeeder;
using Career.Mongo;
using Career.Mvc.Extensions;
using Career.Swagger;
using Definition.Api.Controllers.Base;
using Definition.Api.Extensions;
using Definition.Application;
using Definition.Application.Location.City;
using Definition.Data;
using Definition.Data.DataSeeders.Location;
using Logging;
using Serilog;

var configuration = ConfigurationHelper.GetConfiguration();
Log.Logger = CareerSerilogLoggerFactory.CreateSerilogLogger(configuration);

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    
    services.AddApiVersion();
    services.AddControllersWithARConsistency(configuration);
    services.AddAutoMapper(typeof(CityMappingProfile));
    services.AddMongoContext<DefinitionContext>();
    services.AddMongo();
    services.AddSwagger();
    services.RegisterModule<DefinitionModule>();
    services.RegisterAllTypes<IDataSeeder>(ServiceLifetime.Scoped, typeof(CityDataSeeder));
    services.AddCareerDistributedRedisCache(options => configuration.Bind("Redis", options), typeof(ICityService));
    services.AddCareerConsul(configuration);
    services.AddCareerAuthentication(configuration);
    services.AddCareerAuthorization();

    builder.Host.UseSerilog();
    
    var app = builder.Build();
    app.MigrateDatabase(typeof(DefinitionApiController));
    
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