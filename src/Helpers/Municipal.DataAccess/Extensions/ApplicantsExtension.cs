using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Municipal.DataAccess.Persistence;

namespace Municipal.DataAccess.Extensions;

public static class ApplicantsExtension
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var ucdConnectionString = configuration.GetConnectionString("OrderDb");
        if (!string.IsNullOrEmpty(ucdConnectionString))
            services.AddDbContextPool<OrderDbContext>(options => options.UseSqlServer(ucdConnectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging());

        var classificationsConnectionString = configuration.GetConnectionString("UserManagementDb");
        if (!string.IsNullOrEmpty(ucdConnectionString))
            services.AddDbContextPool<UserManagementDbContext>(options => options
                .UseSqlServer(classificationsConnectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging());

        var blackListConnectionString = configuration.GetConnectionString("FormsDb");
        if (!string.IsNullOrEmpty(ucdConnectionString))
            services.AddDbContextPool<FormsDbContext>(options => options.UseSqlServer(blackListConnectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()); 
        
        var NewsConnectionString = configuration.GetConnectionString("NewsDb");
        if (!string.IsNullOrEmpty(ucdConnectionString))
            services.AddDbContextPool<NewsDbContext>(options => options.UseSqlServer(NewsConnectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging());



        // var requestStatesConnectionString = configuration.GetConnectionString("RequestStatesDb");
        // if (!string.IsNullOrEmpty(ucdConnectionString))
        //     services.AddDbContextPool<RequestsStatesDb>(options => options.UseSqlServer(requestStatesConnectionString)
        //         .EnableDetailedErrors()
        //         .EnableSensitiveDataLogging()); 
        // using var provider = services.BuildServiceProvider();
        // provider.GetService<RequestsStatesDb>()?.Database.Migrate();


    }
}