using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Career.Migration.DataSeeder;

namespace Career.Migration
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TDataSeeder>(this IHost host) where TDataSeeder : IDataSeeder
        {
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            TDataSeeder dataSeeder = services.GetService<TDataSeeder>();
            var logger = services.GetRequiredService<ILogger<TDataSeeder>>();

            try
            {
                dataSeeder.SeedDataAsync().Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
            }

            return host;
        }
        
        public static IHost MigrateDatabase(this IHost host, Type dataSeederAssemblyMarkerType)
        {
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<IDataSeeder>>();
            IEnumerable<IDataSeeder> registeredDataSeeders = services.GetServices<IDataSeeder>();

            var seedDataActions = registeredDataSeeders
                .Select(x => x.SeedDataAsync())
                .ToArray();
            
            try
            {
                Task.WaitAll(seedDataActions);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
            }

            return host;
        }
    }
}
