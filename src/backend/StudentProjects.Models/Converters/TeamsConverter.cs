using StudentProjects.Domain.Entities;
using TeamStudent = StudentProjects.Models.Response.TeamStudent;

namespace StudentProjects.Models.Converters;

public static class TeamsConverter
{
    public static Models.Response.Team ToClientModel(this Team team, bool withStudents = false)
    {
        List<TeamStudent>? students = null;
        if (withStudents)
        {
            students = team.TeamStudents.Select(x => x.ToClientModel()).ToList();
        }
        return new Models.Response.Team(
            team.Id,
            team.Name ?? "",
            team.Description ?? "",
            team.TeamprojectLink ?? "",
            team.ProjectId,
            students);
    }
}