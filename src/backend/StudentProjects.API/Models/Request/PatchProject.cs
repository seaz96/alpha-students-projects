using StudentProjects.Domain.Enums;

namespace StudentProjects.API.Models.Request;

public record PatchProject(string? Name, string? Description, ProjectStatus? Status);