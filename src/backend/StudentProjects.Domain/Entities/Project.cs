using StudentProjects.Domain.Enums;

namespace StudentProjects.Domain.Entities;

public class Project : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid AuthorId { get; set; }
    //todo: add mentors

    public virtual User Author { get; set; }
    public virtual ICollection<Team> Teams { get; set; }
}