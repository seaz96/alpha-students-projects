namespace StudentProjects.Domain.Entities;

public class AccessRight
{
    public Guid UserId { get; set; }
    public Guid ObjectId { get; set; }
    public Uri Right { get; set; }
}