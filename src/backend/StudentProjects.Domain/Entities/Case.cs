namespace StudentProjects.Domain.Entities;

public class Case
{
    public required Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Like> Likes { get; set; }
}