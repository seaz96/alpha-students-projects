using Microsoft.EntityFrameworkCore;
using Minio;
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
        services.AddTransient<MeetingRepository>();
        services.AddTransient<TodosRepository>();
        services.AddTransient<ResultMetasRepository>();
        services.AddTransient<TeamsStudentsRepository>();
        services.AddTransient<StudentPositionsRepository>();

        var dbHost = configuration.GetValue<string>("DATABASE_HOST");
        var dbPassword = configuration.GetValue<string>("DATABASE_PASSWORD");
        var dbName = configuration.GetValue<string>("DATABASE_NAME");
        var dbUser = configuration.GetValue<string>("DATABASE_USER");
        var dbPort = configuration.GetValue<string>("DATABASE_PORT");

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql($"Port={dbPort}; Database={dbName}; Username={dbUser}; Host={dbHost}; Password={dbPassword};");
        });

        var s3PublicKey = configuration.GetValue<string>("S3_PUBLIC_KEY");
        var s3SecretKey = configuration.GetValue<string>("S3_SECRET_KEY");
        var s3Host = configuration.GetValue<string>("S3_HOST");
        var s3Port = configuration.GetValue<int>("S3_PORT");

        services.AddSingleton<IMinioClient>(new MinioClientFactory(client =>
        {
            client.WithCredentials(s3PublicKey, s3SecretKey).WithSSL(false).WithEndpoint(s3Host, s3Port);
        }).CreateClient());
    }

    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();
    }
}