namespace StudentProjects.Domain.Entities;

public class FileObject : BaseEntity
{
    public Guid TeamId { get; set; }
    public required string Name { get; set; }
    public required long Size { get; set; }
}