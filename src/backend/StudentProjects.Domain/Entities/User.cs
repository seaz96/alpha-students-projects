namespace StudentProjects.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }

    public virtual ICollection<AccessRight> AccessRights { get; set; }    
}