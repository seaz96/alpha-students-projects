using StudentProjects.Domain.Enums;

namespace StudentProjects.Models.Request;

public record PatchProject(string? Name, string? Description, ProjectStatus? Status);