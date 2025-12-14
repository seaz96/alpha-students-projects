namespace StudentProjects.Domain.Entities;

public class Team : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public required Guid ProjectId { get; set; }
    public string? TeamprojectLink { get; set; }

    public virtual Project Project { get; set; } = null!;
    public virtual ICollection<TeamStudent> TeamStudents { get; set; } = new List<TeamStudent>();
    public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();
    public virtual ICollection<ResultMeta> ResultMetas { get; set; } = new List<ResultMeta>();
}