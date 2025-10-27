namespace StudentProjects.Domain.Entities;

public class Team
{
    public required Guid Id { get; set; }
    public string Name { get; set; }
    public required Guid ProjectId { get; set; }
    public string TeamprojectLink { get; set; }
    
    public virtual Project Project { get; set; }
    public virtual ICollection<TeamStudent> TeamStudents { get; set; }
    public virtual ICollection<Meeting> Meetings { get; set; }
    public virtual ICollection<Stage> Stages { get; set; }
    public virtual ICollection<ResultMeta> ResultMetas { get; set; }
}