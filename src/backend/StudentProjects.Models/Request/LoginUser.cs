using System.ComponentModel.DataAnnotations;

namespace StudentProjects.Models.Request;

public record LoginUser([EmailAddress] string Email, [MinLength(8)] string Password);