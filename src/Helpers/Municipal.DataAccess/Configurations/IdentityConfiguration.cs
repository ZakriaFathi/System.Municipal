using IdentityServer4.Models;
using Municipal.Application.Legacy.Contracts;

namespace Municipal.DataAccess.Configurations;

public class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                new IdentityResource[] { };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] {
                new ApiScope(IdConstants.Scopes.UserName) { },
                new ApiScope(IdConstants.Scopes.OtpScope)
                {
                    UserClaims = TokenClamis.AddClaims(),
                },
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("SystemMunicipal")
                {
                    Scopes = new List<string>
                    {
                        IdConstants.Scopes.OtpScope,
                        IdConstants.Scopes.UserName
                    },
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId =IdConstants.Clients.Mobile,
                    AllowedGrantTypes =new[] { IdConstants.GrantType.UserCredentials, IdConstants.GrantType.Otp},
                    ClientSecrets = { new Secret("094DF16441FE481D9C4E06AA3BE5E92D800B71249740=4162A5AF64631ABE43A0".Sha256()) },
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
                },
            };
    }