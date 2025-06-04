using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AssistantChatuseridrequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssistantChat_AspNetUsers_UserId",
                table: "AssistantChat");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1368a55c-c27a-4d95-8461-a3914d47b379");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf222b48-48cd-4e97-8f5c-a78e84adecce");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AssistantChat",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a134fbe7-9cee-431b-b4b4-50487ef4f073", null, "Admin", "ADMIN" },
                    { "b5cfa0ce-1415-4714-88d2-7bd740af0d90", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AssistantChat_AspNetUsers_UserId",
                table: "AssistantChat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssistantChat_AspNetUsers_UserId",
                table: "AssistantChat");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a134fbe7-9cee-431b-b4b4-50487ef4f073");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5cfa0ce-1415-4714-88d2-7bd740af0d90");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AssistantChat",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1368a55c-c27a-4d95-8461-a3914d47b379", null, "User", "USER" },
                    { "bf222b48-48cd-4e97-8f5c-a78e84adecce", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AssistantChat_AspNetUsers_UserId",
                table: "AssistantChat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
