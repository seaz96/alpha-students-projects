using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Request;

public record PatchStudents(IEnumerable<Student> Students);