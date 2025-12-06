using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Enums;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using Project = StudentProjects.Domain.Entities.Project;
using User = StudentProjects.Domain.Entities.User;

namespace StudentProjects.Application.Services;

public class ProjectsService(ProjectRepository projectRepository, UserService userService, CaseService caseService)
{
    public async Task<Models.Response.Project> PostAsync(PostProject request)
    {
        var author = await userService.GetAuthorizedUserDtoAsync();
        var convertedFrom = await caseService.GetAsync(request.CaseId);
        if (convertedFrom is null)
            throw new CaseNotFoundException();
        var project = new Project
        {
            Id = Guid.NewGuid(),
            AuthorId = author.Id,
            CreatedAt = DateTime.UtcNow,
            Name = convertedFrom.Name,
            Description = convertedFrom.Description,
            Status = ProjectStatus.Active
        };
        await projectRepository.AddAsync(project);

        return await GetAsync(project.Id);
    }

    public async Task<Models.Response.Project> UpdateAsync(Guid id, PatchProject request)
    {
        var project = await projectRepository.FindTrackedAsync(id);
        if (project is null)
            throw new ProjectNotFoundException();
        project.Name = request.Name;
        project.Description = request.Description;
        project.Status = request.Status ?? project.Status;
        await projectRepository.UpdateAsync(project);
        return project.ToClientModel();
    }

    public async Task<Models.Response.Project> GetAsync(Guid id)
    {
        var project = await projectRepository.FindTrackedAsync(id);
        return project is null
            ? throw new ProjectNotFoundException()
            : project.ToClientModel();
    }

    public async Task<QueryResponse<Models.Response.Project>> QueryAsync(int offset, int limit)
    {
        var projects = await projectRepository.QueryAsync(offset, limit);
        return new QueryResponse<Models.Response.Project>(projects.Data.Select(x => x.ToClientModel()), projects.Count);
    }

    public async Task<Models.Response.Project> UpdateMentorsAsync(Guid projectId, IEnumerable<Guid> mentorIds)
    {
        var mentors = await userService.GetUsersAsync(mentorIds);
        var project = await projectRepository.FindTrackedAsync(projectId);
        if (project is null)
            throw new ProjectNotFoundException();
        //todo: ужасно
        project.Mentors = mentors.Select(x => new User
        {
            Id = x.Id,
            Email = x.Email,
            PasswordHash = string.Empty
        }).ToList();
        await projectRepository.UpdateMentorsAsync(project);
        return await GetAsync(project.Id);
    }
}