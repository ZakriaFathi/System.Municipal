using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Municipal.Application.Core.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Client.IdentityClient;
using Municipal.Client.IdentityClient.Repository;
using Municipal.DataAccess.Repositories;
using Municipal.DataAccess.Repositories.FormRepo;
using Municipal.DataAccess.Repositories.IdentityRepo;
using Municipal.DataAccess.Repositories.NewsRepo;
using Municipal.DataAccess.Repositories.OrderRepo;
using Municipal.DataAccess.Repositories.UserManageRepo;
using Municipal.Utils.Options;

namespace Municipal.DataAccess.Extensions;

public static class Dependency
{
    public static void AddDependency(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        var scope = services.BuildServiceProvider();
        var config = scope.GetService<IConfiguration>();

        var connectivityOptions = config!.GetSection("ComponentConnectivityOptions");
        services.Configure<ComponentConnectivityOptions>(connectivityOptions);
        
        services.IdentityServer(environment, configuration);
        services.AddDbContext(configuration); 

        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
        
        
        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IMailRepository, MailRepository>();
        services.AddTransient<IUserManagmentRepository, UserManageRepository>();
        services.AddTransient<ISherdUserRepository, SherdUserRepository>();
        services.AddTransient<IPermissionsRepository, PermissionsRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IFormRepository, FormRepository>();
        services.AddTransient<IOrdersRepository, OrderRepository>();
        services.AddTransient<IRequestRepository, RequestRepository>();
        services.AddTransient<INewsRepository, NewsRepository>();
        // services.AddTransient<IIdentityClientApi, IdentityServices>();
        // services.AddHttpClient<IdentityClient>();
  
    }
}