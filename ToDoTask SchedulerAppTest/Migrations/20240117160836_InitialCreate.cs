using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTask_SchedulerAppTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Aid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Aid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Tid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Due = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaidAid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Tid);
                    table.ForeignKey(
                        name: "FK_Tasks_Admins_TaidAid",
                        column: x => x.TaidAid,
                        principalTable: "Admins",
                        principalColumn: "Aid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Lid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LuidUid = table.Column<int>(type: "int", nullable: false),
                    LaAidAid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Lid);
                    table.ForeignKey(
                        name: "FK_Logs_Admins_LaAidAid",
                        column: x => x.LaAidAid,
                        principalTable: "Admins",
                        principalColumn: "Aid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_Users_LuidUid",
                        column: x => x.LuidUid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Rid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReminderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RuidUid = table.Column<int>(type: "int", nullable: false),
                    RtidTid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Rid);
                    table.ForeignKey(
                        name: "FK_Reminders_Tasks_RtidTid",
                        column: x => x.RtidTid,
                        principalTable: "Tasks",
                        principalColumn: "Tid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reminders_Users_RuidUid",
                        column: x => x.RuidUid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TasksGiven",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false),
                    Ttid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksGiven", x => new { x.Tuid, x.Ttid });
                    table.ForeignKey(
                        name: "FK_TasksGiven_Tasks_Ttid",
                        column: x => x.Ttid,
                        principalTable: "Tasks",
                        principalColumn: "Tid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TasksGiven_Users_Tuid",
                        column: x => x.Tuid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LaAidAid",
                table: "Logs",
                column: "LaAidAid");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LuidUid",
                table: "Logs",
                column: "LuidUid");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_RtidTid",
                table: "Reminders",
                column: "RtidTid");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_RuidUid",
                table: "Reminders",
                column: "RuidUid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaidAid",
                table: "Tasks",
                column: "TaidAid");

            migrationBuilder.CreateIndex(
                name: "IX_TasksGiven_Ttid",
                table: "TasksGiven",
                column: "Ttid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "TasksGiven");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
