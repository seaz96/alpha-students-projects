using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Enums;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
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
            CreatedAt = DateTime.UtcNow
        };
        await caseRepository.AddAsync(entity);
        return await GetAsync(entity.Id);
    }

    public async Task<Case?> PatchAsync(Guid id, PatchCase request)
    {
        var entity = await caseRepository.GetByIdAsync(id);
        if (entity is null)
            throw new CaseNotFoundException();
        entity.Name = request.Name;
        entity.Description = request.Description;
        await caseRepository.UpdateAsync(entity);
        return entity.ToResponse();
    }

    public async Task<Case?> UpdateStatusAsync(Guid id, CaseStatus status)
    {
        var entity = await caseRepository.GetByIdAsync(id);
        if (entity is null)
            throw new CaseNotFoundException();
        entity.Status = status;
        await caseRepository.UpdateAsync(entity);
        return entity.ToResponse();
    }

    public async Task<IEnumerable<Case>> GetAsync(CommonQuery request)
    {
        var cases = await caseRepository.QueryAsync(request.Offset, request.Limit);
        return cases.Select(x => x.ToResponse());
    }

    public async Task<Case?> GetAsync(Guid id)
    {
        var entity = await caseRepository.GetByIdAsync(id);
        return entity?.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await caseRepository.DeleteByIdAsync(id);
    }
}