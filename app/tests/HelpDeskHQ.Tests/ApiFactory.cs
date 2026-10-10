using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HelpDeskHQ.Tests;

public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        // Fake values, these tests never reach the database.
        builder.UseSetting("ConnectionStrings:DefaultConnection", "Host=localhost;Database=not_used");
        builder.UseSetting("Jwt:Key", "test-only-key-with-at-least-32-characters");
    }
}