using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Municipal.Application.Identity.Configurations;
using Municipal.Application.Identity.IdentityValidator;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Models;
namespace Municipal.DataAccess.Extensions;

public static class IdentityExtension
    {
        public static IServiceCollection IdentityServer(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {

            void SqlServerOptionsAction(SqlServerDbContextOptionsBuilder options)
            {
                options.MigrationsAssembly("Municipal.DataAccess");
            }

            services.AddDbContext<IdentityUsersDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                dbContextOptionsBuilder.UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("IdentityConnection"),
                    SqlServerOptionsAction);

            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityUsersDbContext>();


            var rsa = new RsaKeyService(environment, TimeSpan.FromDays(30));
            services.AddTransient<RsaKeyService>(_ => rsa);

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryClients(IdentityConfiguration.Clients)
            .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
            .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
            .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
            .AddAspNetIdentity<AppUser>()
            .AddCustomTokenRequestValidator<RegistrationResponse>()
            //.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddExtensionGrantValidator<UserCredentialsValidator>()
            .AddExtensionGrantValidator<OtpValidator>()
            //.AddProfileService<ProfileService>()
            .AddSigningCredential(rsa.GetKey(), SecurityAlgorithms.RsaSha256);


            using var serviceProvider = services.BuildServiceProvider();
            using var appDbContext = serviceProvider.GetService<IdentityUsersDbContext>();
            appDbContext?.Database.Migrate();

            return services;
        }
    }