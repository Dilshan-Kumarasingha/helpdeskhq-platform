namespace HelpDeskHQ.Api.Models;

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // New tickets always start as Open.
    public TicketStatus Status { get; set; } = TicketStatus.Open;

    // Stored in UTC so the time does not depend on the server's time zone.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}