using StudentProjects.API.Models.Dtos;

namespace StudentProjects.API.Models.Response;

public record CaseResponse(
    Guid Id,
    string Name,
    string Description,
    UserDto Author,
    DateTime CreatedAt,
    int Likes,
    int Dislikes);