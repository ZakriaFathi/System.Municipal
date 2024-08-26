using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Municipal.Application.Identity.Contracts;
using Municipal.Utils.Enums;

namespace Municipal.DataAccess.Extensions;

public static class ExtensionsPolicy
{
    public static void AddAuthentications(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.Authority = configuration["JWT:Authority"];
                options.Audience = configuration["JWT:Audience"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidTypes = new[] { "at+jwt" },              
                };
            });
        
        services.AddAuthorization(options =>
        {
            #region Authentication
            
            options.AddPolicy(PolicyAuthorize.SingIn, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.UserName);
            }); 
            
            options.AddPolicy(PolicyAuthorize.RefreshToken, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
            });
            
            options.AddPolicy(PolicyAuthorize.ChangePassword, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
                policyUser.RequireClaim("UserType", 
                    UserType.SuperAdmin.ToString(),
                                        UserType.Maker.ToString(),
                                        UserType.SubMaker.ToString());
                policyUser.RequireClaim("UserManagement",
                    PermissionNames.ChangePassword.ToString());
            }); 
            
            options.AddPolicy(PolicyAuthorize.ResetPassword, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
            });   
            
            options.AddPolicy(PolicyAuthorize.ForgotPassword, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
            });

            #endregion

            #region UserManagement

            options.AddPolicy(PolicyAuthorize.GetUsers, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
                policyUser.RequireClaim("UserType", 
                    UserType.SuperAdmin.ToString(),
                                        UserType.Maker.ToString());
                policyUser.RequireClaim("UserManagement",
                    PermissionNames.GetUsers.ToString());
            }); 
            
            options.AddPolicy(PolicyAuthorize.CreateUser, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
                policyUser.RequireClaim("UserType", 
                    UserType.SuperAdmin.ToString(),
                                        UserType.Maker.ToString());
                policyUser.RequireClaim("UserManagement",
                    PermissionNames.CreateUser.ToString());
            }); 
            
            options.AddPolicy(PolicyAuthorize.ChangeUserActivation, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
                policyUser.RequireClaim("UserType", 
                    UserType.SuperAdmin.ToString(),
                                        UserType.Maker.ToString());
                policyUser.RequireClaim("UserManagement",
                    PermissionNames.ChangeUserActivation.ToString());
            });   
            
            options.AddPolicy(PolicyAuthorize.UpdateUserProfile, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
                policyUser.RequireClaim("UserType", 
                    UserType.SuperAdmin.ToString(),
                                        UserType.Maker.ToString());
                policyUser.RequireClaim("UserManagement",
                    PermissionNames.GetUserProfile.ToString());
            }); 
            
            options.AddPolicy(PolicyAuthorize.UpdateUserKyc, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
                policyUser.RequireClaim("UserType", 
                    UserType.SuperAdmin.ToString(),
                                        UserType.Maker.ToString());
                policyUser.RequireClaim("UserManagement",
                    PermissionNames.GetUserProfile.ToString());
            });  
            
            options.AddPolicy(PolicyAuthorize.UserClaims, policyUser =>
            {
                policyUser.RequireClaim(JwtClaimTypes.Scope, IdConstants.Scopes.OtpScope);
                policyUser.RequireClaim("UserType", 
                    UserType.SuperAdmin.ToString(),
                                        UserType.Maker.ToString());
                policyUser.RequireClaim("UserManagement",
                    PermissionNames.GetUserRolesAndPermissions.ToString());
            });    

            #endregion
 
   
        });
    }
}