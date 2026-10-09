namespace HelpDeskHQ.Api.Models;

// The three kinds of people who use the helpdesk.
// The order must stay fixed, because the numbers are saved in the database.
public enum UserRole
{
    Employee,
    Agent,
    Admin
}