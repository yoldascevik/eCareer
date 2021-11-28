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
                // shared scope
                new ApiScope("read", "Can read all your data."),
                new ApiScope("write", "Can write your data."),
                new ApiScope("delete", "Can delete your data."),
                new ApiScope("manage", "Can full access your data.")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new("definition", "Definition API")
                {
                    Scopes = { "read", "write", "delete", "manage" },
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
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
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
                        IdentityServerConstants.StandardScopes.Email,
                        "read", "write", "delete", "manage"
                    }
                }
            };
    }
}