using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AssistantChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f62617b-e649-4999-9852-a224cd3c3d38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37538a92-519b-4bc3-bc54-81560c06d993");

            migrationBuilder.CreateTable(
                name: "AssistantChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SenderType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistantChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistantChat_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1368a55c-c27a-4d95-8461-a3914d47b379", null, "User", "USER" },
                    { "bf222b48-48cd-4e97-8f5c-a78e84adecce", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssistantChat_UserId",
                table: "AssistantChat",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssistantChat");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1368a55c-c27a-4d95-8461-a3914d47b379");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf222b48-48cd-4e97-8f5c-a78e84adecce");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f62617b-e649-4999-9852-a224cd3c3d38", null, "User", "USER" },
                    { "37538a92-519b-4bc3-bc54-81560c06d993", null, "Admin", "ADMIN" }
                });
        }
    }
}
