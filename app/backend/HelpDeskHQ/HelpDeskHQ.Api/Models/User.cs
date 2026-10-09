namespace HelpDeskHQ.Api.Models;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    // The email is the login name, so it must be unique.
    public string Email { get; set; } = string.Empty;

    // Only the hash is stored. The real password is never saved.
    public string PasswordHash { get; set; } = string.Empty;

    // New users start as Employee. An Admin can promote them later.
    public UserRole Role { get; set; } = UserRole.Employee;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}