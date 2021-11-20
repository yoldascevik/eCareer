using Career.Identity.Data;
using Career.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddCareerIdentityContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CareerIdentityDbContext>(options => 
                options.UseNpgsql(configuration.GetConnectionString("IdentityServer")));
            
            return services;
        }
        
        public static IServiceCollection AddCareerIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Lockout.AllowedForNewUsers = false;
                    options.Lockout.MaxFailedAccessAttempts = 20;
                })
                .AddEntityFrameworkStores<CareerIdentityDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}