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
                new IdentityResources.Profile(),
                new("roles", "Your role(s)", new List<string>() { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("cv", "CV management."),
                new ApiScope("job", "Job management."),
                new ApiScope("company", "Company management."),
                new ApiScope("definition", "Definition management.")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new("companyapi", "Company API", new[] { "role" })
                {
                    Scopes = { "company" },
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                },
                new("cvapi", "CV API", new[] { "role" })
                {
                    Scopes = { "cv" },
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                },
                new("definitionapi", "Definition API", new[] { "role" })
                {
                    Scopes = { "definition" },
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                },
                new("jobapi", "Job API", new[] { "role" })
                {
                    Scopes = { "job" },
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
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType = AccessTokenType.Jwt,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "company",
                        "cv",
                        "definition",
                        "job"
                    }
                }
            };
    }
}