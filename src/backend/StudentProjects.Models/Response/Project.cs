using StudentProjects.Domain.Enums;

namespace StudentProjects.Models.Response;

public record Project(
    Guid Id,
    string Name,
    string Description,
    ProjectStatus Status,
    DateTime CreatedAt,
    User Author,
    IEnumerable<User> Mentors);