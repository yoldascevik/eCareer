using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Career.Identity.Constants
{
    public static class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("definition.read", "Definition api data read"),
                new ApiScope("definition.write", "Definition api data write"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new("definition", "Definition API")
                {
                    Scopes = { "definition.read", "definition.write" },
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "career.client",
                    ClientName = "Career Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "definition.read", 
                        "definition.write"
                    }
                }
            };
    }
}