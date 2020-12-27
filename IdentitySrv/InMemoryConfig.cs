using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace IdentitySrv
{
    public static class InMemoryConfig
    {
        public static IEnumerable<Client> Clients()
        {
            return new[] {
                new Client()
                {
                    ClientId ="Clnt01",
                    ClientSecrets= new []{ new Secret("Secretpassword".Sha256()) },
                    AllowedScopes = new[]{ "weatherApiUSER", "apiWrite", StandardScopes.OpenId,StandardScopes.Email },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AccessTokenType = AccessTokenType.Reference
               },
                new Client()
                {
                    ClientId ="Clnt02_JWT",
                    ClientSecrets= new []{ new Secret("Secretpassword2".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new[]{ "weatherApiUSER", StandardScopes.OpenId,StandardScopes.Email,StandardScopes.Profile }
                },
                 new Client()
                {
                    ClientId ="Clnt03_RFT",
                    ClientSecrets= new []{ new Secret("Secretpassword3".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new[]{ "weatherApiUSER", "apiWrite", StandardScopes.OpenId,StandardScopes.Email,StandardScopes.Profile },
                    AccessTokenType = AccessTokenType.Reference
                }
            };
        }

        internal static List<TestUser> Users()
        {
            return new List<TestUser> {
                new TestUser()
                {
                    SubjectId="596fe1ee-2bb8-4c81-82e7-5a091ff8c81e",
                    Username="user",
                    Password="password",
                    Claims =
                    {
                        new Claim(type: JwtClaimTypes.Name,value: "Roger"),
                        new Claim(type: JwtClaimTypes.Email,value: "roger@reader.com"),
                        new Claim(type: JwtClaimTypes.Role,value: "user"),
                        new Claim(type: JwtClaimTypes.WebSite,value: "www.example.com"),
                        new Claim("Authorization","API_READ")
                    }
                },
                new TestUser()
                {
                    SubjectId="5b3f5ac9-3970-4807-b516-f5a5429476aa",
                    Username="admin",
                    Password="admin",
                    Claims =
                    {
                        new Claim(type: JwtClaimTypes.Name,value: "James"),
                        new Claim(type: JwtClaimTypes.Email,value: "James@reader.com"),
                        new Claim(type: JwtClaimTypes.Role,value: "admin"),
                        new Claim(type: JwtClaimTypes.WebSite,value: "www.example.com"),
                        new Claim("Authorization","API_READ"),
                         new Claim("Authorization","API_ADMIN")
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> Scopes()
        {
            return new[] {
                new ApiScope(){Name ="weatherApiUSER" },
                 new ApiScope(){Name ="apiWrite" }
            };
        }

        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("WeatherAPISecure")
                {
                    Scopes = new List<string>{ "weatherApiUSER" },
                    ApiSecrets = new List<Secret>{new Secret("scopeSecret".Sha256())},
                    UserClaims = new List<string>{"role"}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
           return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name="role",
                    UserClaims= new List<string>{"role"}
                }
            };
        }
}
}
