using System.ComponentModel.DataAnnotations;

namespace StudentProjects.Models.Request;

public record RegisterUser([EmailAddress] string Email, [MinLength(8)] string Password);