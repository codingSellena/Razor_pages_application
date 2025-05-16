using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class initagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResultClasses_TestClasses_TestId",
                table: "TestResultClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestResultClasses",
                table: "TestResultClasses");

            migrationBuilder.RenameTable(
                name: "TestResultClasses",
                newName: "TestResultClass");

            migrationBuilder.RenameIndex(
                name: "IX_TestResultClasses_TestId",
                table: "TestResultClass",
                newName: "IX_TestResultClass_TestId");

            migrationBuilder.AddColumn<int>(
                name: "TestReportId",
                table: "TestResultClass",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestResultClass",
                table: "TestResultClass",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TestReportClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestReportId = table.Column<int>(type: "int", nullable: true),
                    TestTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestReportClass", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResultClass_TestReportId",
                table: "TestResultClass",
                column: "TestReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultClass_TestClasses_TestId",
                table: "TestResultClass",
                column: "TestId",
                principalTable: "TestClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultClass_TestReportClass_TestReportId",
                table: "TestResultClass",
                column: "TestReportId",
                principalTable: "TestReportClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResultClass_TestClasses_TestId",
                table: "TestResultClass");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultClass_TestReportClass_TestReportId",
                table: "TestResultClass");

            migrationBuilder.DropTable(
                name: "TestReportClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestResultClass",
                table: "TestResultClass");

            migrationBuilder.DropIndex(
                name: "IX_TestResultClass_TestReportId",
                table: "TestResultClass");

            migrationBuilder.DropColumn(
                name: "TestReportId",
                table: "TestResultClass");

            migrationBuilder.RenameTable(
                name: "TestResultClass",
                newName: "TestResultClasses");

            migrationBuilder.RenameIndex(
                name: "IX_TestResultClass_TestId",
                table: "TestResultClasses",
                newName: "IX_TestResultClasses_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestResultClasses",
                table: "TestResultClasses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultClasses_TestClasses_TestId",
                table: "TestResultClasses",
                column: "TestId",
                principalTable: "TestClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
