using StudentProjects.Domain.Enums;

namespace StudentProjects.Domain.Entities;

public class Case : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public CaseStatus Status { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }
    public virtual User Author { get; set; }
}