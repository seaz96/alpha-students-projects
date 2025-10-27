using StudentProjects.Domain.Enums;

namespace StudentProjects.Domain.Entities;

public class User
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }
}