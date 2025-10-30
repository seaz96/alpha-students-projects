using System.ComponentModel.DataAnnotations;

namespace StudentProjects.API.Models.Request;

public record LoginRequest([EmailAddress] string Email, [MinLength(8)] string Password);