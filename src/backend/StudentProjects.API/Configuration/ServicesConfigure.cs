using StudentProjects.Application.Services;

namespace StudentProjects.API.Configuration;

public static class ServicesConfigure
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<UserService>();
        services.AddTransient<CaseService>();
        services.AddTransient<ReviewsService>();
    }
}