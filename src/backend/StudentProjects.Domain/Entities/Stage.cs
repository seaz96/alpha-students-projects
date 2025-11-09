namespace StudentProjects.Domain.Entities;

public class Stage : BaseEntity
{
    public required Guid TeamId { get; set; }
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? UrfuScore { get; set; }
    public string? UrfuComment { get; set; }
    public int? MentorScore { get; set; }
    public string? MentorComment { get; set; }
    
    public virtual Team Team { get; set; }
}