namespace StudentProjects.Domain.Entities;

public class Todo
{
    public required Guid Id { get; set; }
    public bool Checked { get; set; }
    public string Content { get; set; }
    public Guid? ParentId { get; set; }
    public required Guid MeetingId { get; set; }
}