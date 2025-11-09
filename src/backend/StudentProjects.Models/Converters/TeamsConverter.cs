using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class TeamsConverter
{
    public static Models.Response.Team ToClientModel(this Team team)
    {
        return new Models.Response.Team(
            team.Id,
            team.Name ?? "",
            team.Description ?? "",
            team.TeamprojectLink ?? "",
            team.ProjectId);
    }
}