using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchEngineAPI.Migrations
{
    /// <inheritdoc />
    public partial class adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password" },
                values: new object[] { new Guid("570ae72a-f266-4d4b-a207-366cf4d98a16"), "danielmamre@gmail.com", "dm1234dm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("570ae72a-f266-4d4b-a207-366cf4d98a16"));
        }
    }
}
