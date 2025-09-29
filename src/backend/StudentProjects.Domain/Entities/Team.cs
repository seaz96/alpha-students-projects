namespace StudentProjects.Domain.Entities;

public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Student> Students { get; set; }
    public virtual ICollection<Meeting> Meetings { get; set; }
}