using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class ProjectConverter
{
    public static Models.Response.Project ToClientModel(this Project project)
    {
        return new Models.Response.Project(
            project.Id,
            project.Name ?? "",
            project.Description ?? "",
            project.Status,
            project.CreatedAt,
            project.Author.ToDto(),
            project.Mentors.Select(x => x.ToDto()));
    }
}