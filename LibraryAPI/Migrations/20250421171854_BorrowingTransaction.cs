using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class BorrowingTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10ff5503-12bc-433a-b9f8-86d7733527f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24db6a2c-080f-4cbf-8b0d-efebf58d70b8");

            migrationBuilder.CreateTable(
                name: "BorrowingTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookCopyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowingTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowingTransaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowingTransaction_BookCopy_BookCopyId",
                        column: x => x.BookCopyId,
                        principalTable: "BookCopy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f62617b-e649-4999-9852-a224cd3c3d38", null, "User", "USER" },
                    { "37538a92-519b-4bc3-bc54-81560c06d993", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingTransaction_BookCopyId",
                table: "BorrowingTransaction",
                column: "BookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingTransaction_UserId",
                table: "BorrowingTransaction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowingTransaction");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f62617b-e649-4999-9852-a224cd3c3d38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37538a92-519b-4bc3-bc54-81560c06d993");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10ff5503-12bc-433a-b9f8-86d7733527f7", null, "User", "USER" },
                    { "24db6a2c-080f-4cbf-8b0d-efebf58d70b8", null, "Admin", "ADMIN" }
                });
        }
    }
}
