using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Entities;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;

namespace StudentProjects.Application.Services;

public class StagesService(StageRepository stageRepository)
{
    public async Task<Models.Response.Stage> CreateAsync(PostStage request)
    {
        var stage = new Stage
        {
            Id = Guid.NewGuid(),
            TeamId = request.TeamId,
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };
        
        await stageRepository.AddAsync(stage);
        return stage.ToClientModel();
    }

    public async Task<Models.Response.Stage> UpdateAsync(Guid id, PatchStage request)
    {
        var stage = await stageRepository.FindTrackedAsync(id);

        if (stage is null)
            throw new StageNotFoundException();
        
        stage.Name = request.Name;
        stage.StartDate = request.StartDate;
        stage.EndDate = request.EndDate;
        stage.UrfuComment = request.UrfuComment;
        stage.MentorComment = request.MentorComment;
        stage.UrfuScore = request.UrfuScore;
        stage.MentorScore = request.MentorScore;
        
        await stageRepository.UpdateAsync(stage);
        return stage.ToClientModel();
    }

    public async Task<List<Models.Response.Stage>> QueryAsync(QueryStages request)
    {
        return (await stageRepository.QueryAsync(request.TeamId, request.Offset, request.Limit))
            .Select(x => x.ToClientModel())
            .ToList();
    }

    public async Task<Models.Response.Stage> GetAsync(Guid stageId)
    {
        return (await stageRepository.FindTrackedAsync(stageId) ?? throw new StageNotFoundException()).ToClientModel();
    }
}