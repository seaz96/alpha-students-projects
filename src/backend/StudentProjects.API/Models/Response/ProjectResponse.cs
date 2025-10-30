using StudentProjects.API.Models.Dtos;
using StudentProjects.Domain.Enums;

namespace StudentProjects.API.Models.Response;

public record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    ProjectStatus Status,
    DateTime CreatedAt,
    UserDto Author);