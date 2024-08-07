using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Municipal.Application.Legacy.Abstracts;
using Municipal.DataAccess.Repositories;
using Municipal.Utils.Options;

namespace Municipal.DataAccess.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IMailRepository, MailRepository>();
        services.AddTransient<IIdentityManagementService, IdentityManagementRepository>();

        services.IdentityServer(environment, configuration);

        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

        return services;

    }
}