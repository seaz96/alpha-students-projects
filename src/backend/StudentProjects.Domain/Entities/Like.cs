namespace StudentProjects.Domain.Entities;

public class Like
{
    public required Guid CaseId { get; set; }
    public required Guid UserId { get; set; }
    public bool Dislike { get; set; }
    public string Comment { get; set; }
}