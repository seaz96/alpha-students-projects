using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using ResultMeta = StudentProjects.Domain.Entities.ResultMeta;
using Team = StudentProjects.Domain.Entities.Team;

namespace StudentProjects.Application.Services;

public class TeamsService(TeamsRepository teamsRepository, ResultMetasRepository resultMetasRepository)
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
            : team.ToClientModel(true);
    }

    public async Task<bool> ExistsAsync(Guid teamId)
    {
        var team = await teamsRepository.FindTrackedAsync(teamId);
        return team is not null;
    }

    public async Task<QueryResponse<Models.Response.Team>> QueryAsync(QueryTeams request)
    {
        var teams = await teamsRepository.QueryAsync(request.ProjectId, request.Offset, request.Limit);
        return new QueryResponse<Models.Response.Team>(teams.Data.Select(x => x.ToClientModel()), teams.Count);
    }

    public async Task<Models.Response.Team> DeleteAsync(Guid id)
    {
        var project = await teamsRepository.FindTrackedAsync(id) ?? throw new ProjectNotFoundException();
        await teamsRepository.DeleteAsync(project);
        return project.ToClientModel();
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

    public async Task<Models.Response.ResultMeta> UpdateResultAsync(Guid teamId, PutTeamResult request)
    {
        var meta = await resultMetasRepository.FindTrackedByTeamIdAsync(teamId)
                   ?? new ResultMeta
                   {
                       Id = Guid.NewGuid(),
                       TeamId = teamId
                   };
        meta.Comment = request.Comment ?? meta.Comment;
        meta.Score = request.Score;
        await resultMetasRepository.AddOrUpdateAsync(meta);
        return meta.ToClientModel();
    }

    public async Task<Models.Response.ResultMeta> GetResultAsync(Guid teamId)
    {
        var meta = await resultMetasRepository.FindTrackedByTeamIdAsync(teamId);
        return meta is null ? throw new ResultMetaNotFoundException() : meta.ToClientModel();
    }
}