using StudentProjects.Domain.Enums;

namespace StudentProjects.ClientModels.Request;

public record PatchProject(string? Name, string? Description, ProjectStatus? Status);