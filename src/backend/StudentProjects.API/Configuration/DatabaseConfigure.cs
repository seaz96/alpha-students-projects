using Microsoft.EntityFrameworkCore;
using StudentProjects.API.Data;

namespace StudentProjects.API.Configuration;

public static class DatabaseConfigure
{
    public static void ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dbHost = configuration.GetValue<string>("DATABASE_HOST");
        var dbPassword = configuration.GetValue<string>("DATABASE_PASSWORD");
        var dbName = configuration.GetValue<string>("DATABASE_NAME");
        var dbUser = configuration.GetValue<string>("DATABASE_USER");
        var dbPort = configuration.GetValue<string>("DATABASE_PORT");

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql($"Port={dbPort}; Database={dbName}; Username={dbUser}; Host={dbHost}; Password={dbPassword};");
        }, ServiceLifetime.Transient);
    }
}