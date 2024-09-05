using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Municipal.Application.Identity.Contracts;
using Municipal.Utils.Options;

namespace Municipal.Application.Legacy.Configurations;

public static class IdentityConfiguration
    { 
        
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope(IdConstants.Scopes.UserName) { },
                new ApiScope(IdConstants.Scopes.OtpScope)
                {
                    UserClaims = TokenClamis.AddClaims(),
                },
            };
        }
        
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("System.Municipal")
                {
                    Scopes = new List<string>
                    {
                        IdConstants.Scopes.OtpScope,
                        IdConstants.Scopes.UserName
                    },
                }
            };
        }
        
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId =IdConstants.Clients.Mobile,
                    AllowedGrantTypes =new[] { IdConstants.GrantType.UserCredentials, IdConstants.GrantType.Otp},
                    ClientSecrets = { new Secret(configuration["ClientSettings:ClientSecrets"].Sha256()) },
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    AlwaysSendClientClaims = true,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    AllowAccessTokensViaBrowser = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = 3600,
                    SlidingRefreshTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowedScopes = {
                        IdConstants.Scopes.OtpScope,
                        IdConstants.Scopes.UserName
                    }
                }
            };
        }
    }