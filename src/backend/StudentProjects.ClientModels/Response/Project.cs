using StudentProjects.Domain.Enums;

namespace StudentProjects.ClientModels.Response;

public record Project(
    Guid Id,
    string Name,
    string Description,
    ProjectStatus Status,
    DateTime CreatedAt,
    User Author);