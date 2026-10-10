using HelpDeskHQ.Api.Data;
using HelpDeskHQ.Api.Models;

namespace HelpDeskHQ.Api.Jobs;

public class SlaJob
{
    private readonly AppDbContext database;
    private readonly IConfiguration configuration;
    private readonly ILogger<SlaJob> logger;

    public SlaJob(AppDbContext database, IConfiguration configuration, ILogger<SlaJob> logger)
    {
        this.database = database;
        this.configuration = configuration;
        this.logger = logger;
    }

    public void EscalateOldTickets()
    {
        // Kept in settings so i can set a tiny value when testing.
        var hours = int.Parse(configuration["Sla:EscalateAfterHours"] ?? "24");
        var limit = DateTime.UtcNow.AddHours(-hours);

        var lateTickets = database.Tickets
            .Where(ticket => ticket.CreatedAt < limit)
            .Where(ticket => !ticket.IsEscalated)
            .Where(ticket => ticket.Status == TicketStatus.Open || ticket.Status == TicketStatus.InProgress)
            .ToList();

        foreach (var ticket in lateTickets)
        {
            ticket.IsEscalated = true;
        }

        database.SaveChanges();

        logger.LogInformation("SLA job escalated {Count} ticket(s).", lateTickets.Count);
    }
}