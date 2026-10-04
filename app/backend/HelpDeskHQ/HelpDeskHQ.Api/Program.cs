using HelpDeskHQ.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Read the database connection string from the settings.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// If the setting is missing, stop with a clear message.
if (connectionString == null)
{
	throw new Exception("Database connection string is missing.");
}

// Tell the app to use PostgreSQL.
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(connectionString));

// Add a health check that tests the database connection.
builder.Services.AddHealthChecks()
	.AddDbContextCheck<AppDbContext>();

var app = builder.Build();

// "Is the app running?" This does not touch the database.
app.MapGet("/health/live", () => "Healthy");

// "Is the app ready to work?" This also checks the database.
app.MapHealthChecks("/health/ready");

// Shows which version is running.
// The version comes from the APP_VERSION environment variable.
// If it is not set, we show "dev".
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