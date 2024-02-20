using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTasksModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Tauid",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Tauid",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Tauid",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tauid",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Tauid",
                table: "Tasks",
                column: "Tauid");

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
