using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddEventArtistRelation : Migration
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
                    { "563f7810-8283-4149-87c3-ccefafa3e131", null, "User", "USER" },
                    { "ce33fb3c-6751-4ea0-91ef-36a135dbbe95", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
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
                keyValue: "563f7810-8283-4149-87c3-ccefafa3e131");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce33fb3c-6751-4ea0-91ef-36a135dbbe95");

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
    }
}
