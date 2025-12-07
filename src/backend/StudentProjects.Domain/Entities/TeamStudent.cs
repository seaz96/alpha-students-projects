namespace StudentProjects.Domain.Entities;

public class TeamStudent
{
    public required Guid TeamId { get; set; }
    public required Guid StudentId { get; set; }
    public string? AcademicGroup { get; set; }
    public Guid? PositionId { get; set; }
    public StudentPosition? Position { get; set; } 

    public virtual Team Team { get; set; }
    public virtual Student Student { get; set; }
}