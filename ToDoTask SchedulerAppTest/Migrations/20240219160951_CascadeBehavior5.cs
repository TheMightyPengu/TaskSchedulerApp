using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class CascadeBehavior5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders");

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
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders",
                column: "Rtid",
                principalTable: "Tasks",
                principalColumn: "Tid",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
