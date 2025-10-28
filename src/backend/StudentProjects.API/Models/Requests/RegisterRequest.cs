using System.ComponentModel.DataAnnotations;

namespace StudentProjects.API.Models.Requests;

public record RegisterRequest([EmailAddress] string Email, [MinLength(8)] string Password);