namespace HelpDeskHQ.Api.Models;

// The steps a ticket goes through from start to finish.
public enum TicketStatus
{
	Open,
	InProgress,
	Resolved,
	Closed
}