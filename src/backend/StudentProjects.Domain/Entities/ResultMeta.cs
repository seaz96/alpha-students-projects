namespace StudentProjects.Domain.Entities;

public class ResultMeta : BaseEntity
{
    public required Guid TeamId { get; set; }
    public int? Score { get; set; }
    public string? Comment { get; set; }
    
    public virtual Team Team { get; set; }
}