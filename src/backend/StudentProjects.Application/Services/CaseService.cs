using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Enums;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using Case = StudentProjects.Models.Response.Case;

namespace StudentProjects.Application.Services;

public class CaseService(CaseRepository caseRepository, UserService userService)
{
    public async Task<Case?> AddAsync(PostCase request)
    {
        var author = await userService.GetAuthorizedUserDtoAsync();

        var entity = new Domain.Entities.Case
        {
            Id = Guid.NewGuid(),
            AuthorId = author.Id,
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            Type = CaseType.Request
        };
        await caseRepository.AddAsync(entity);
        return await GetAsync(entity.Id);
    }

    public async Task<Case?> PatchAsync(Guid id, PatchCase request)
    {
        var user = await userService.GetAuthorizedUserDtoAsync();
        var entity = await caseRepository.FindTrackedAsync(id);

        if (entity is null)
            throw new CaseNotFoundException();
        if (entity.AuthorId != user.Id)
            throw new ForbiddenException();

        entity.Name = request.Name;
        entity.Description = request.Description;
        await caseRepository.UpdateAsync(entity);
        return entity.ToResponse();
    }

    public async Task<Case?> UpdateStatusAsync(Guid id, CaseType type)
    {
        var entity = await caseRepository.FindTrackedAsync(id);
        if (entity is null)
            throw new CaseNotFoundException();
        entity.Type = type;
        await caseRepository.UpdateAsync(entity);
        return entity.ToResponse();
    }

    public async Task<QueryResponse<Case>> GetAsync(QueryCases request)
    {
        var cases = await caseRepository.QueryAsync(request.Offset, request.Limit, request.Type);
        return new QueryResponse<Case>(cases.Data.Select(x => x.ToResponse()), cases.Count);
    }

    public async Task<Case?> GetAsync(Guid id)
    {
        var entity = await caseRepository.FindTrackedAsync(id);
        return entity?.ToResponse();
    }

    public async Task<Case> DeleteAsync(Guid id)
    {
        var entity = await caseRepository.FindTrackedAsync(id);
        if (entity is null)
            throw new CaseNotFoundException();
        await caseRepository.DeleteAsync(entity);
        return entity.ToResponse();
    }
}