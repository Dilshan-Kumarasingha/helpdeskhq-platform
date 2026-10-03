// Program.cs
// This is where the API starts. 

// 1. Create the app builder.
//    It reads settings from appsettings.json and from environment variables.
var builder = WebApplication.CreateBuilder(args);

// 2. Turn on the health check feature.
builder.Services.AddHealthChecks();

// 3. Build the app.
var app = builder.Build();

// 4. Health check endpoints.
//    Docker and Kubernetes will call these to ask "are you OK?".
//    Right now both do the same thing.

app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready");

// 5. A small endpoint that shows which version is running.
//    The version comes from an environment variable called APP_VERSION.
//    If it is not set, we show "dev".
app.MapGet("/api/info", () =>
{
    var version = app.Configuration["APP_VERSION"] ?? "dev";

    return new
    {
        name = "HelpDeskHQ.Api",
        version = version
    };
});

// 6. Start listening for requests.
app.Run();