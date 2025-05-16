using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class changedeletebehaviors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestClasses_DeviceClasses_DeviceId",
                table: "TestClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultClasses_TestClasses_TestId",
                table: "TestResultClasses");

            migrationBuilder.AddForeignKey(
                name: "FK_TestClasses_DeviceClasses_DeviceId",
                table: "TestClasses",
                column: "DeviceId",
                principalTable: "DeviceClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultClasses_TestClasses_TestId",
                table: "TestResultClasses",
                column: "TestId",
                principalTable: "TestClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestClasses_DeviceClasses_DeviceId",
                table: "TestClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultClasses_TestClasses_TestId",
                table: "TestResultClasses");

            migrationBuilder.AddForeignKey(
                name: "FK_TestClasses_DeviceClasses_DeviceId",
                table: "TestClasses",
                column: "DeviceId",
                principalTable: "DeviceClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultClasses_TestClasses_TestId",
                table: "TestResultClasses",
                column: "TestId",
                principalTable: "TestClasses",
                principalColumn: "Id");
        }
    }
}
