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
        services.AddTransient<TeamsService>();
        services.AddTransient<StudentsService>();
        services.AddTransient<ProjectsService>();
        services.AddTransient<StagesService>();
        services.AddTransient<MeetingsService>();
        services.AddTransient<TodoService>();
    }
}