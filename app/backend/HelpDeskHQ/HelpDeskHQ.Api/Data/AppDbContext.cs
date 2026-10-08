using HelpDeskHQ.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskHQ.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Each DbSet becomes one table in PostgreSQL.
    public DbSet<Ticket> Tickets => Set<Ticket>();
}