using System.ComponentModel.DataAnnotations;

namespace StudentProjects.API.Models.Request;

public record LoginUser([EmailAddress] string Email, [MinLength(8)] string Password);