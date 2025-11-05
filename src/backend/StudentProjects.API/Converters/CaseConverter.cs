using Case = StudentProjects.ClientModels.Response.Case;

namespace StudentProjects.API.Converters;

public static class CaseConverter
{
    public static Case ToResponse(this Domain.Entities.Case entity, int? likes = null, int? dislikes = null)
    {
        return new Case(
            entity.Id,
            entity.Name ?? "",
            entity.Description ?? "",
            entity.Author.ToDto(),
            entity.CreatedAt,
            likes ?? 0,
            dislikes ?? 0);
    }
}