using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookAuthorPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_BookId",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookAuthor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                columns: new[] { "BookId", "AuthorId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookAuthor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_BookId",
                table: "BookAuthor",
                column: "BookId");
        }
    }
}
