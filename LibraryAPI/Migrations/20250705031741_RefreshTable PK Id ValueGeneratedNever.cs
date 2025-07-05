using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTablePKIdValueGeneratedNever : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1639e109-6953-4303-99d4-396b072a892c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2912ce60-b73a-4865-8b18-c74c055f40ef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2350855d-d0de-4717-8bd8-59096a29cdc9", null, "User", "USER" },
                    { "73ad1929-f4ab-4685-9cbb-b23e94f89150", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2350855d-d0de-4717-8bd8-59096a29cdc9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73ad1929-f4ab-4685-9cbb-b23e94f89150");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1639e109-6953-4303-99d4-396b072a892c", null, "Admin", "ADMIN" },
                    { "2912ce60-b73a-4865-8b18-c74c055f40ef", null, "User", "USER" }
                });
        }
    }
}
