using System.ComponentModel.DataAnnotations;

namespace StudentProjects.Domain.Entities;

public class BaseEntity
{
    [Key]
    public required Guid Id { get; set; }
}