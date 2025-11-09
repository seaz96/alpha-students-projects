namespace StudentProjects.Domain.Entities;

public class Student : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Telegram { get; set; }
    public Guid? PositionId { get; set; }

    public virtual ICollection<TeamStudent> TeamStudents { get; set; }
    public virtual StudentPosition? Position { get; set; }
}