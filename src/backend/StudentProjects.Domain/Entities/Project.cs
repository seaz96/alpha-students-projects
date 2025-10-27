using StudentProjects.Domain.Enums;

namespace StudentProjects.Domain.Entities;

public class Project
{
    public required Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}