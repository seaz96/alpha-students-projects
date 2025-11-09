using StudentProjects.Domain.Enums;

namespace StudentProjects.Models.Response;

public record Case(
    Guid Id,
    string Name,
    string Description,
    User Author,
    DateTime CreatedAt,
    int Likes,
    int Dislikes,
    CaseStatus Status);