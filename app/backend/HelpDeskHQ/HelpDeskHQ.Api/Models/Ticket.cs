namespace HelpDeskHQ.Api.Models;

// A Ticket is one support request.
public class Ticket
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public TicketStatus Status { get; set; } = TicketStatus.Open;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}