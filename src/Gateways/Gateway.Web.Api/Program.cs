using Gateway.Web.Api.Helpers;
using Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Serilog;

var configuration = ConfigurationHelper.GetConfiguration();
Log.Logger = CareerSerilogLoggerFactory.CreateSerilogLogger(configuration);

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;

    services.AddControllers();
    services.AddOcelot(configuration)
        .AddConsul()
        .AddPolly();
    
    services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()));
   
    services.AddSwaggerForOcelot(configuration);
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseCors();
    app.UseSwaggerForOcelotUI();
    app.UseRouting();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    app.UseOcelot().Wait();

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