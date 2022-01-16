using Career.Identity.Constants;
using Career.Identity.Models;
using Career.Identity.Quickstart;
using Career.Identity.Services;

namespace Career.Identity.Extensions;

public static class IdentityServerExtensions
{
    public static IServiceCollection AddCareerIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
            .AddInMemoryApiResources(IdentityConfig.ApiResources)
            .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
            .AddInMemoryClients(IdentityConfig.Clients)
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<ProfileService>()
            .AddTestUsers(TestUsers.Users);
            
        builder.AddDeveloperSigningCredential();

        return services;
    }
}