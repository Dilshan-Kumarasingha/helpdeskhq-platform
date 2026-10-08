using System.ComponentModel.DataAnnotations;

namespace HelpDeskHQ.Api.Dtos;

// Only the fields a user may send. Id, Status and CreatedAt are set by the server.
public class CreateTicketRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;
}