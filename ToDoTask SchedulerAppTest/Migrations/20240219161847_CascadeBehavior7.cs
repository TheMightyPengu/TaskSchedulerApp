using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class CascadeBehavior7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_AspNetUsers_Rauid",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_AspNetUsers_Rauid",
                table: "Reminders",
                column: "Rauid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders",
                column: "Rtid",
                principalTable: "Tasks",
                principalColumn: "Tid",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_AspNetUsers_Rauid",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_AspNetUsers_Rauid",
                table: "Reminders",
                column: "Rauid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders",
                column: "Rtid",
                principalTable: "Tasks",
                principalColumn: "Tid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
