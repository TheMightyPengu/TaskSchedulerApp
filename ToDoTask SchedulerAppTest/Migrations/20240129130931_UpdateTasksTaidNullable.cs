using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTasksTaidNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Admins_TaidAid",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TaidAid",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Admins_TaidAid",
                table: "Tasks",
                column: "TaidAid",
                principalTable: "Admins",
                principalColumn: "Aid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Admins_TaidAid",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TaidAid",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Admins_TaidAid",
                table: "Tasks",
                column: "TaidAid",
                principalTable: "Admins",
                principalColumn: "Aid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
