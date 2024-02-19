using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class CascadeBehavior2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksGiven_AspNetUsers_TGauid",
                table: "TasksGiven");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksGiven_AspNetUsers_TGauid",
                table: "TasksGiven",
                column: "TGauid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksGiven_AspNetUsers_TGauid",
                table: "TasksGiven");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksGiven_AspNetUsers_TGauid",
                table: "TasksGiven",
                column: "TGauid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
