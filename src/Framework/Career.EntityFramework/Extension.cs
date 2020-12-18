using System;
using Career.EntityFramework.UnitOfWork;
using Career.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Career.EntityFramework
{
    public static class Extension
    {
        /// <summary>
        /// Add generic unit of work for multiple dbcontext.
        /// </summary>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IUnitOfWork<>), typeof(EfUnitOfWork<>));
        }
        
        /// <summary>
        /// Add unit of work for specific dbcontext.
        /// </summary>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext: DbContext
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(EfUnitOfWork<TContext>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(EfUnitOfWork<>));

            return services;
        }

        public static IHost MigrateEntityFrameworkDatabase<TContext>(this IHost host) where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                var context = services.GetRequiredService<TContext>();
                context.Database.Migrate();

                logger.LogInformation("Database migration completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrate the DB.");
            }

            return host;
        }
    }
}