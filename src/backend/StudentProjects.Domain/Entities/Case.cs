namespace StudentProjects.Domain.Entities;

public class Case
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public short Year { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Team> Teams { get; set; }
}