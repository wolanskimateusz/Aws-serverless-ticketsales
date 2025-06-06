using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class ArtistsUpdatev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f863f55-09c3-4503-84f1-98228efb0250");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afb2367a-e3cf-4e0c-aac3-9cded01dda28");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f67b5b5-cef6-4d0b-b75b-a20c6ac26dc9", null, "Admin", "ADMIN" },
                    { "7f3c6750-f4c2-4da2-ac0d-17db591401eb", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f67b5b5-cef6-4d0b-b75b-a20c6ac26dc9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f3c6750-f4c2-4da2-ac0d-17db591401eb");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Artists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f863f55-09c3-4503-84f1-98228efb0250", null, "Admin", "ADMIN" },
                    { "afb2367a-e3cf-4e0c-aac3-9cded01dda28", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
