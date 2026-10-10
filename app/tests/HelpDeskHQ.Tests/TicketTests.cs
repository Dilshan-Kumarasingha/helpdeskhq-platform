using HelpDeskHQ.Api.Models;

namespace HelpDeskHQ.Tests;

public class TicketTests
{
    [Fact]
    public void NewTicket_StartsOpenAndNotEscalated()
    {
        var ticket = new Ticket();

        Assert.Equal(TicketStatus.Open, ticket.Status);
        Assert.False(ticket.IsEscalated);
    }
}