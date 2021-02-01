using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsPlus.Migrations
{
    public partial class EventTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTypes_Events_EventID",
                table: "EventTypes");

            migrationBuilder.DropIndex(
                name: "IX_EventTypes_EventID",
                table: "EventTypes");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "EventTypes");

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "EventTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventTypes_EventID",
                table: "EventTypes",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTypes_Events_EventID",
                table: "EventTypes",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
