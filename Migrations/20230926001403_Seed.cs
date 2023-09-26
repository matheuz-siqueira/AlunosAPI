using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlunosAPI.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1, "user@example.com", "User" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 2, "name@example.com", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
