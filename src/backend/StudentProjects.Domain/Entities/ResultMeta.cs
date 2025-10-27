namespace StudentProjects.Domain.Entities;

public class ResultMeta
{
    public required Guid Id { get; set; }
    public required Guid TeamId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
}