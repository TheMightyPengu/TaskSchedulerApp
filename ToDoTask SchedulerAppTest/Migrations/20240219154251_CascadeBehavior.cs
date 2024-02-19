using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class CascadeBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Tasks_RtaskTid",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksGiven_Tasks_TGtid",
                table: "TasksGiven");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_RtaskTid",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "RtaskTid",
                table: "Reminders");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_Rtid",
                table: "Reminders",
                column: "Rtid");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders",
                column: "Rtid",
                principalTable: "Tasks",
                principalColumn: "Tid",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksGiven_Tasks_TGtid",
                table: "TasksGiven",
                column: "TGtid",
                principalTable: "Tasks",
                principalColumn: "Tid",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Tasks_Rtid",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksGiven_Tasks_TGtid",
                table: "TasksGiven");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_Rtid",
                table: "Reminders");

            migrationBuilder.AddColumn<int>(
                name: "RtaskTid",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_RtaskTid",
                table: "Reminders",
                column: "RtaskTid");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Tasks_RtaskTid",
                table: "Reminders",
                column: "RtaskTid",
                principalTable: "Tasks",
                principalColumn: "Tid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksGiven_Tasks_TGtid",
                table: "TasksGiven",
                column: "TGtid",
                principalTable: "Tasks",
                principalColumn: "Tid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
