namespace StudentProjects.Domain.Entities;

public class Meeting
{
    public required Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Summary { get; set; }
    public string RecordLink { get; set; }
    public required Guid TeamId { get; set; }
    public int Score { get; set; }
    public Guid? Previous { get; set; }
    public Guid? Next { get; set; }
}