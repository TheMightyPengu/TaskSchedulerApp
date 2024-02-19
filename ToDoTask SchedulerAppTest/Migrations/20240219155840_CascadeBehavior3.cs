using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class CascadeBehavior3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Tauid",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Tauid",
                table: "Tasks",
                column: "Tauid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Tauid",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Tauid",
                table: "Tasks",
                column: "Tauid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
