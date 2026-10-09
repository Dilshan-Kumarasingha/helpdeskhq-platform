using HelpDeskHQ.Api.Data;
using HelpDeskHQ.Api.Dtos;
using HelpDeskHQ.Api.Models;
using HelpDeskHQ.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskHQ.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext database;
    private readonly TokenService tokenService;

    public AuthController(AppDbContext database, TokenService tokenService)
    {
        this.database = database;
        this.tokenService = tokenService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        // Emails are saved in lowercase so Anna@x.com and anna@x.com are the same login.
        var email = request.Email.Trim().ToLower();

        var emailTaken = database.Users.Any(user => user.Email == email);

        if (emailTaken)
        {
            return Conflict("This email is already registered.");
        }

        var newUser = new User();
        newUser.FullName = request.FullName;
        newUser.Email = email;
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        database.Users.Add(newUser);
        database.SaveChanges();

        // The hash must never leave the server, so only safe fields are returned.
        return Created("/api/users/" + newUser.Id, new
        {
            id = newUser.Id,
            fullName = newUser.FullName,
            email = newUser.Email,
            role = newUser.Role
        });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var email = request.Email.Trim().ToLower();

        var user = database.Users.FirstOrDefault(u => u.Email == email);

        // Same answer for a wrong email and a wrong password, so nobody can probe which emails exist.
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized("Email or password is wrong.");
        }

        var token = tokenService.CreateToken(user);

        return Ok(new
        {
            token = token,
            email = user.Email,
            role = user.Role
        });
    }
}