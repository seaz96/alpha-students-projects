namespace StudentProjects.Domain.Entities;

public class Meeting : BaseEntity
{
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public string? Summary { get; set; }
    public string? RecordLink { get; set; }
    public required Guid TeamId { get; set; }
    public int? Score { get; set; }
    public Guid? PreviousId { get; set; }
    public Guid? NextId { get; set; }

    public virtual Team Team { get; set; }
    public virtual ICollection<Todo> Todos { get; set; }
}