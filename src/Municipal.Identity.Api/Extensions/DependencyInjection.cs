using Municipal.Application.Identity.Abstracts;
using Municipal.DataAccess.Repositories.IdentityRepo;
using Municipal.DataAccess.Repositories;
using Municipal.Utils.Options;
using Municipal.DataAccess.Extensions;

namespace Municipal.Identity.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IMailRepository, MailRepository>();
        services.AddTransient<IIdentityManagementRepository, IdentityManagementRepository>();

        services.IdentityServer(environment, configuration);

        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

        return services;

    }
}