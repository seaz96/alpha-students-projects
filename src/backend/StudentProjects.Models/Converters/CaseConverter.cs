using Case = StudentProjects.Models.Response.Case;

namespace StudentProjects.Models.Converters;

public static class CaseConverter
{
    public static Case ToResponse(this Domain.Entities.Case entity)
    {
        return new Case(
            entity.Id,
            entity.Name ?? "",
            entity.Description ?? "",
            entity.Author.ToDto(),
            entity.CreatedAt,
            entity.Reviews.Count(x => !x.Dislike),
            entity.Reviews.Count(x => x.Dislike),
            entity.Type);
    }
}