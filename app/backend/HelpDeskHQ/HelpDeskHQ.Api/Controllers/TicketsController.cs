using HelpDeskHQ.Api.Data;
using HelpDeskHQ.Api.Dtos;
using HelpDeskHQ.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskHQ.Api.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext database;

    public TicketsController(AppDbContext database)
    {
        this.database = database;
    }

    [HttpGet]
    public IActionResult GetAllTickets()
    {
        // Newest tickets first, because that is what a support agent wants to see.
        var tickets = database.Tickets
            .OrderByDescending(ticket => ticket.CreatedAt)
            .ToList();

        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public IActionResult GetTicket(int id)
    {
        var ticket = database.Tickets.Find(id);

        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpPost]
    public IActionResult CreateTicket(CreateTicketRequest request)
    {
        // Only Title and Description come from the request. The server sets the rest.
        var newTicket = new Ticket();
        newTicket.Title = request.Title;
        newTicket.Description = request.Description;

        database.Tickets.Add(newTicket);
        database.SaveChanges();

        return Created("/api/tickets/" + newTicket.Id, newTicket);
    }

    [HttpPut("{id}/status")]
    public IActionResult UpdateStatus(int id, UpdateTicketStatusRequest request)
    {
        var ticket = database.Tickets.Find(id);

        if (ticket == null)
        {
            return NotFound();
        }

        ticket.Status = request.Status;
        database.SaveChanges();

        return Ok(ticket);
    }
}