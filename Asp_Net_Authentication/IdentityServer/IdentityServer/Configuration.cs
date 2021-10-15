using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name="an.scope",
                UserClaims =
                {
                    "an.grandma"
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("ApiOne", "My Api", new string[] { "an.api.grandma" }),
             new ApiScope("ApiTwo", "Client serving as Api")
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                // scopes that client has access to
                AllowedScopes = { "ApiOne" }
            },

            //MvcClient
            new Client
            {
                ClientId = "client_id_mvc",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce=true,
                ClientSecrets ={ new Secret("client_secret_mvc".Sha256())},

                RedirectUris ={ "https://localhost:44308/signin-oidc" },
                PostLogoutRedirectUris ={ "https://localhost:44308/Home/Index" },

                AllowedScopes =
                {
                    "ApiOne",
                    "ApiTwo",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "an.scope"
                },
                RequireConsent=false,
                AllowOfflineAccess=true
                //puts all the claims in the id token
                //AlwaysIncludeUserClaimsInIdToken = true
            },

            //JavaScript Client
            new Client
            {
                ClientId="client_id_js",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce=true,
                RequireClientSecret=false,

                RedirectUris ={ "https://localhost:44310/home/signin" },
                AllowedCorsOrigins = { "https://localhost:44310" },
                PostLogoutRedirectUris ={ "https://localhost:44310/Home/Index" },

                AllowedScopes =
                {
                    "ApiOne",
                    "ApiTwo",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "an.scope"
                },
                //AccessTokenLifetime =1,
                AllowAccessTokensViaBrowser=true,
                RequireConsent=false,
                AllowOfflineAccess=true

            }
        };
    }
}
