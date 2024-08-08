using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Client.IdentityClient;
using Municipal.Client.IdentityClient.Repository;
using Municipal.DataAccess.Repositories.FormRepo;
using Municipal.DataAccess.Repositories.OrderRepo;
using Municipal.DataAccess.Repositories.UserManageRepo;
using Municipal.Utils.Options;

namespace Municipal.DataAccess.Extensions;

public static class Dependency
{
    public static void AddDependency(this IServiceCollection services)
    {
        var scope = services.BuildServiceProvider();
        var config = scope.GetService<IConfiguration>();

        var connectivityOptions = config!.GetSection("ComponentConnectivityOptions");
        services.Configure<ComponentConnectivityOptions>(connectivityOptions);
        
        services.AddTransient<IUserManagmentRepository, UserManageRepository>();
        services.AddTransient<IPermissionsRepository, PermissionsRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IFormRepository, FormRepository>();
        services.AddTransient<IOrdersRepository, OrderRepository>();
        services.AddTransient<IIdentityClientApi, IdentityServices>();
        services.AddHttpClient<IdentityClient>();
  
    }
}