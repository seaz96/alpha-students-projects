using StudentProjects.API.Models.Response;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Converters;

public static class CaseConverter
{
    public static CaseResponse ToResponse(this Case entity, int? likes = null, int? dislikes = null)
    {
        return new CaseResponse(
            entity.Id,
            entity.Name ?? "",
            entity.Description ?? "",
            entity.Author.ToDto(),
            entity.CreatedAt,
            likes ?? 0,
            dislikes ?? 0);
    }
}