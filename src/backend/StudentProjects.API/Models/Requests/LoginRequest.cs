using System.ComponentModel.DataAnnotations;

namespace StudentProjects.API.Models.Requests;

public record LoginRequest([EmailAddress] string Email, [MinLength(8)] string Password);