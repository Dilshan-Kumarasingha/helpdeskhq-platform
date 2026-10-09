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

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Two users must never share the same email, so the database enforces it.
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();
    }
}