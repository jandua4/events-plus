using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsPlus.Migrations
{
    public partial class ConstraintTweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Events_EventID",
                table: "Attendees");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Managers_ManagerID",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Events_EventID",
                table: "Attendees",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Managers_ManagerID",
                table: "Events",
                column: "ManagerID",
                principalTable: "Managers",
                principalColumn: "ManagerID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Events_EventID",
                table: "Attendees");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Managers_ManagerID",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Events_EventID",
                table: "Attendees",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Managers_ManagerID",
                table: "Events",
                column: "ManagerID",
                principalTable: "Managers",
                principalColumn: "ManagerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
