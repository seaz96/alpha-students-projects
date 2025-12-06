using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using Stage = StudentProjects.Domain.Entities.Stage;

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

    public async Task<QueryResponse<Models.Response.Stage>> QueryAsync(QueryStages request)
    {
        var response = await stageRepository.QueryAsync(request.TeamId, request.Offset, request.Limit);
        return new QueryResponse<Models.Response.Stage>(response.Data.Select(x => x.ToClientModel()), response.Count);
    }

    public async Task<Models.Response.Stage> GetAsync(Guid stageId)
    {
        return (await stageRepository.FindTrackedAsync(stageId) ?? throw new StageNotFoundException()).ToClientModel();
    }

    public async Task<Models.Response.Stage> DeleteAsync(Guid stageId)
    {
        var stage = await stageRepository.FindTrackedAsync(stageId) ?? throw new StageNotFoundException();
        await stageRepository.DeleteAsync(stage);
        return stage.ToClientModel();
    }
}