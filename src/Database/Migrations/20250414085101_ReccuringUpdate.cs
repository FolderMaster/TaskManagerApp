using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class ReccuringUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeIntervals_TaskElements_TaskElementId",
                table: "TimeIntervals");

            migrationBuilder.DropColumn(
                name: "ExecutedReal",
                table: "TaskElements");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "TaskElements");

            migrationBuilder.DropColumn(
                name: "SpentTime",
                table: "TaskElements");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TaskElements");

            migrationBuilder.RenameColumn(
                name: "TaskElementId",
                table: "TimeIntervals",
                newName: "TaskElementExecutionId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeIntervals_TaskElementId",
                table: "TimeIntervals",
                newName: "IX_TimeIntervals_TaskElementExecutionId");

            migrationBuilder.CreateTable(
                name: "RecurringTaskElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    RecurringSettings_Frequency = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    RecurringSettings_Cycle_Minutes = table.Column<ulong>(type: "INTEGER", nullable: false),
                    RecurringSettings_Cycle_Hours = table.Column<uint>(type: "INTEGER", nullable: false),
                    RecurringSettings_Cycle_WeekDays = table.Column<byte>(type: "INTEGER", nullable: false),
                    RecurringSettings_Cycle_MonthDays = table.Column<uint>(type: "INTEGER", nullable: false),
                    RecurringSettings_Cycle_Months = table.Column<ushort>(type: "INTEGER", nullable: false),
                    RecurringSettings_StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RecurringSettings_EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedExecutionsDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTaskElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTaskElements_TaskElements_Id",
                        column: x => x.Id,
                        principalTable: "TaskElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskElementExecutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Progress = table.Column<double>(type: "REAL", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    SpentTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ExecutedReal = table.Column<double>(type: "REAL", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TaskElementId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskElementExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskElementExecutions_TaskElements_TaskElementId",
                        column: x => x.TaskElementId,
                        principalTable: "TaskElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskElementExecutions_TaskElementId",
                table: "TaskElementExecutions",
                column: "TaskElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeIntervals_TaskElementExecutions_TaskElementExecutionId",
                table: "TimeIntervals",
                column: "TaskElementExecutionId",
                principalTable: "TaskElementExecutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeIntervals_TaskElementExecutions_TaskElementExecutionId",
                table: "TimeIntervals");

            migrationBuilder.DropTable(
                name: "RecurringTaskElements");

            migrationBuilder.DropTable(
                name: "TaskElementExecutions");

            migrationBuilder.RenameColumn(
                name: "TaskElementExecutionId",
                table: "TimeIntervals",
                newName: "TaskElementId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeIntervals_TaskElementExecutionId",
                table: "TimeIntervals",
                newName: "IX_TimeIntervals_TaskElementId");

            migrationBuilder.AddColumn<double>(
                name: "ExecutedReal",
                table: "TaskElements",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Progress",
                table: "TaskElements",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SpentTime",
                table: "TaskElements",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TaskElements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeIntervals_TaskElements_TaskElementId",
                table: "TimeIntervals",
                column: "TaskElementId",
                principalTable: "TaskElements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
