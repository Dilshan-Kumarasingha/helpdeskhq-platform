using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskHQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketIsEscalated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEscalated",
                table: "Tickets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEscalated",
                table: "Tickets");
        }
    }
}
