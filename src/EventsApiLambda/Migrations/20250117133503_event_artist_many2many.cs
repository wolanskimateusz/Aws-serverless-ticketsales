using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class event_artist_many2many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_EventId",
                table: "Artists");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bad1c6ff-645a-417e-be15-391fa4be86b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4649059-77c6-4d01-b971-2c3cfa885638");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Artists");

            migrationBuilder.CreateTable(
                name: "ArtistEvent",
                columns: table => new
                {
                    ArtistsId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistEvent", x => new { x.ArtistsId, x.EventId });
                    table.ForeignKey(
                        name: "FK_ArtistEvent_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "012806d8-c8c3-4a8e-b68d-57e0439884e9", null, "User", "USER" },
                    { "6ea7ce62-ee0a-4731-9197-eced11d004d8", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistEvent_EventId",
                table: "ArtistEvent",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistEvent");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "012806d8-c8c3-4a8e-b68d-57e0439884e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ea7ce62-ee0a-4731-9197-eced11d004d8");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Artists",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bad1c6ff-645a-417e-be15-391fa4be86b6", null, "User", "USER" },
                    { "c4649059-77c6-4d01-b971-2c3cfa885638", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_EventId",
                table: "Artists",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
