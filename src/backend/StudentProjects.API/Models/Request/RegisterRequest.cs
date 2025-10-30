using System.ComponentModel.DataAnnotations;

namespace StudentProjects.API.Models.Request;

public record RegisterRequest([EmailAddress] string Email, [MinLength(8)] string Password);