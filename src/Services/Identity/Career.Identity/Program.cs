using Career.Configuration;
using Career.Identity.Extensions;
using Logging;
using Serilog;

var configuration = ConfigurationHelper.GetConfiguration();
Log.Logger = CareerSerilogLoggerFactory.CreateSerilogLogger(configuration);

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    
    services.AddControllersWithViews();
    services.AddCareerIdentityContext(configuration);
    services.AddCareerIdentity();
    services.AddCareerIdentityServer(configuration);
    services.AddCareerConsul(configuration);
    services.AddAuthentication();

    builder.Host.UseSerilog();
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseStaticFiles();
    app.UseRouting();
    app.UseIdentityServer();
    app.UseAuthorization();
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