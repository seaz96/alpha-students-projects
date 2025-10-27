namespace StudentProjects.Domain.Entities;

public class Team
{
    public required Guid Id { get; set; }
    public string Name { get; set; }
    public required Guid ProjectId { get; set; }
    public string TeamprojectLink { get; set; }
}