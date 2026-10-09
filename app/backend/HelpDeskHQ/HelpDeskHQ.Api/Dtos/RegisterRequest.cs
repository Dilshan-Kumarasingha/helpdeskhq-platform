using System.ComponentModel.DataAnnotations;

namespace HelpDeskHQ.Api.Dtos;

// Only the fields a new user may send. The role is not here, so nobody can pick their own.
public class RegisterRequest
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}