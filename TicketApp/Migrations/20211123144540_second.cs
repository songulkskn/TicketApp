using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketApp.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descsiption",
                table: "Tickets",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tickets",
                newName: "Descsiption");
        }
    }
}
