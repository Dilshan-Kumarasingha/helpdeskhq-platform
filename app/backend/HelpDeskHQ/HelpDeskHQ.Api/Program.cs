using HelpDeskHQ.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// The connection string comes from user secrets on my machine
// and from an environment variable when the app runs in a container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new Exception("ConnectionStrings:DefaultConnection is not set.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

var app = builder.Build();

app.MapControllers();

// Liveness only says the app is running, so it does not check the database.
app.MapGet("/health/live", () => "Healthy");

// Readiness also checks the database connection.
app.MapHealthChecks("/health/ready");

// Shows which version is running. APP_VERSION is set at deploy time.
app.MapGet("/api/info", () =>
{
    var version = app.Configuration["APP_VERSION"] ?? "dev";

    return new
    {
        name = "HelpDeskHQ.Api",
        version = version
    };
});

app.Run();