using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsPlus.Migrations
{
    public partial class tweak3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Events_EventID",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_EventID",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Managers");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ManagerID",
                table: "Events",
                column: "ManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Managers_ManagerID",
                table: "Events",
                column: "ManagerID",
                principalTable: "Managers",
                principalColumn: "ManagerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Managers_ManagerID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ManagerID",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_EventID",
                table: "Managers",
                column: "EventID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Events_EventID",
                table: "Managers",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
