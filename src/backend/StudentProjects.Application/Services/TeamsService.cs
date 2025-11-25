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
            ProjectId = request.ProjectId,
            TeamprojectLink = request.TeamprojectLink
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

    public async Task<List<Models.Response.Team>> QueryAsync(QueryTeams request)
    {
        var teams = await teamsRepository.QueryAsync(request.ProjectId, request.Offset, request.Limit);
        return teams.Select(x => x.ToClientModel()).ToList();
    }

    public async Task<Models.Response.Team> UpdateAsync(Guid teamId, PatchTeam request)
    {
        var team = await teamsRepository.FindTrackedAsync(teamId);
        if (team is null) throw new TeamNotFoundException();
        team.Description = request.Description;
        team.Name = request.Name;
        team.TeamprojectLink = request.TeamprojectLink;
        await teamsRepository.UpdateAsync(team);
        return await GetAsync(teamId);
    }
}