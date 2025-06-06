using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class ArtistsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3148e520-b9b7-4df9-8bd5-ce278e866ccd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6fd4f29-9f8a-4772-8412-aa421d9f4fd5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f863f55-09c3-4503-84f1-98228efb0250", null, "Admin", "ADMIN" },
                    { "afb2367a-e3cf-4e0c-aac3-9cded01dda28", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f863f55-09c3-4503-84f1-98228efb0250");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afb2367a-e3cf-4e0c-aac3-9cded01dda28");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3148e520-b9b7-4df9-8bd5-ce278e866ccd", null, "Admin", "ADMIN" },
                    { "f6fd4f29-9f8a-4772-8412-aa421d9f4fd5", null, "User", "USER" }
                });
        }
    }
}
