using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Entities;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;

namespace StudentProjects.Application.Services;

public class TeamsService(TeamsRepository teamsRepository)
{
    public async Task<Models.Response.Team> CreateAsync(PostTeam request)
    {
        var team = new Team
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            Name = request.Name,
            ProjectId = request.ProjectId
        };
        await teamsRepository.AddAsync(team);
        return await GetAsync(team.Id);
    }

    public async Task<Models.Response.Team> GetAsync(Guid teamId)
    {
        var team = await teamsRepository.FindTrackedAsync(teamId);
        return team is null
            ? throw new TeamNotFoundException()
            : team.ToClientModel();
    }
}