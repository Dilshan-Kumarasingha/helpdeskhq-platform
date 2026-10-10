using HelpDeskHQ.Api.Data;
using HelpDeskHQ.Api.Jobs;
using HelpDeskHQ.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace HelpDeskHQ.Tests;

public class SlaJobTests
{
    private AppDbContext CreateDatabase()
    {
        // A new database name for every test, so tests do not share data.
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private SlaJob CreateJob(AppDbContext database)
    {
        var settings = new Dictionary<string, string?>();
        settings["Sla:EscalateAfterHours"] = "24";

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        return new SlaJob(database, configuration, NullLogger<SlaJob>.Instance);
    }

    [Fact]
    public void OldOpenTicket_GetsEscalated()
    {
        var database = CreateDatabase();
        var ticket = new Ticket();
        ticket.CreatedAt = DateTime.UtcNow.AddHours(-30);
        database.Tickets.Add(ticket);
        database.SaveChanges();

        CreateJob(database).EscalateOldTickets();

        Assert.True(database.Tickets.First().IsEscalated);
    }

    [Fact]
    public void NewTicket_IsNotEscalated()
    {
        var database = CreateDatabase();
        var ticket = new Ticket();
        ticket.CreatedAt = DateTime.UtcNow.AddHours(-2);
        database.Tickets.Add(ticket);
        database.SaveChanges();

        CreateJob(database).EscalateOldTickets();

        Assert.False(database.Tickets.First().IsEscalated);
    }

    [Fact]
    public void OldResolvedTicket_IsNotEscalated()
    {
        var database = CreateDatabase();
        var ticket = new Ticket();
        ticket.CreatedAt = DateTime.UtcNow.AddHours(-30);
        ticket.Status = TicketStatus.Resolved;
        database.Tickets.Add(ticket);
        database.SaveChanges();

        CreateJob(database).EscalateOldTickets();

        Assert.False(database.Tickets.First().IsEscalated);
    }
}