using Microsoft.EntityFrameworkCore;
using StudentProjects.DataLayer;
using StudentProjects.DataLayer.Repositories;

namespace StudentProjects.API.Configuration;

public static class DatabaseConfigure
{
    public static void ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<UserRepository>();
        services.AddTransient<CaseRepository>();
        services.AddTransient<ReviewsRepository>();
        services.AddTransient<TeamsRepository>();
        services.AddTransient<StudentsRepository>();
        services.AddTransient<ProjectRepository>();
        services.AddTransient<StageRepository>();

        var dbHost = configuration.GetValue<string>("DATABASE_HOST");
        var dbPassword = configuration.GetValue<string>("DATABASE_PASSWORD");
        var dbName = configuration.GetValue<string>("DATABASE_NAME");
        var dbUser = configuration.GetValue<string>("DATABASE_USER");
        var dbPort = configuration.GetValue<string>("DATABASE_PORT");

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql($"Port={dbPort}; Database={dbName}; Username={dbUser}; Host={dbHost}; Password={dbPassword};");
        });
    }

    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();
    }
}