namespace HelpDeskHQ.Api.Models;

// A ticket moves forward through these states, from Open to Closed.
public enum TicketStatus
{
    Open,
    InProgress,
    Resolved,
    Closed
}