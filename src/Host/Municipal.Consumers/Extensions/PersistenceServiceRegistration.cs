using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Municipal.Consumers.Persistence;

namespace Municipal.Consumers.Extensions;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddDbServices(this IServiceCollection services)
    {
        void SqlServerOptionsAction(SqlServerDbContextOptionsBuilder options)
        {
            options.MigrationsAssembly("Municipal.Consumers");
        }
        services.AddDbContext<RequestsStatesDb>((serviceProvider, dbContextOptionsBuilder) =>
        {
            dbContextOptionsBuilder.UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("RequestStatesDb"),
                SqlServerOptionsAction);
        });

        using var serviceProvider = services.BuildServiceProvider();
        using var appDbContext = serviceProvider.GetService<RequestsStatesDb>();
        appDbContext?.Database.Migrate();
        
            
        return services;
    }
}