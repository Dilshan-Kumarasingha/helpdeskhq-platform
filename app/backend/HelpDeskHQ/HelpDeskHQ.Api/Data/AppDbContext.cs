using HelpDeskHQ.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskHQ.Api.Data;

// AppDbContext is connection to the database.
// The app uses it to read and save data.
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	// Each DbSet becomes one table in the database.

	// This one is the "Tickets" table.
	public DbSet<Ticket> Tickets => Set<Ticket>();
}