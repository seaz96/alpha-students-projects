namespace StudentProjects.Domain.Entities;

public class Student
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Telegram { get; set; }

    public virtual ICollection<TeamStudent> TeamStudents { get; set; }
}