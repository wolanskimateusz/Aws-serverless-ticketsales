using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class EventUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7aa4d7d-a8c0-4825-ab2a-966507abf90c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc4726ca-71f3-4d69-a73f-172cfcb62210");

            migrationBuilder.AddColumn<int>(
                name: "NormalPrice",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VipPrice",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bad1c6ff-645a-417e-be15-391fa4be86b6", null, "User", "USER" },
                    { "c4649059-77c6-4d01-b971-2c3cfa885638", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bad1c6ff-645a-417e-be15-391fa4be86b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4649059-77c6-4d01-b971-2c3cfa885638");

            migrationBuilder.DropColumn(
                name: "NormalPrice",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VipPrice",
                table: "Events");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c7aa4d7d-a8c0-4825-ab2a-966507abf90c", null, "User", "USER" },
                    { "dc4726ca-71f3-4d69-a73f-172cfcb62210", null, "Admin", "ADMIN" }
                });
        }
    }
}
