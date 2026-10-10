using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using HelpDeskHQ.Api.Data;
using HelpDeskHQ.Api.Jobs;
using HelpDeskHQ.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// The connection string comes from user secrets on my machine
// and from an environment variable when the app runs in a container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new Exception("ConnectionStrings:DefaultConnection is not set.");
}

var jwtKey = builder.Configuration["Jwt:Key"];

if (jwtKey == null)
{
    throw new Exception("Jwt:Key is not set.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<SlaJob>();

// Hangfire needs a real PostgreSQL, so the integration tests leave it out.
if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddHangfire(config =>
        config.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));
    builder.Services.AddHangfireServer();
}

// The API accepts a token only if the signature, issuer, audience and expiry are all valid.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

var app = builder.Build();

// Authentication must come before authorization, because the API has to know who you are first.
app.UseAuthentication();
app.UseAuthorization();

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

if (!app.Environment.IsEnvironment("Testing"))
{
    var jobManager = app.Services.GetRequiredService<IRecurringJobManager>();
    jobManager.AddOrUpdate<SlaJob>("sla-escalation", job => job.EscalateOldTickets(), "*/5 * * * *");
}

app.Run();

// The test project needs this to start the API.
public partial class Program
{
}