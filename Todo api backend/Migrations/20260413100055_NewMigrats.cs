using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_api_backend.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_AuthorId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Title",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AuthorId_Title",
                table: "Categories",
                columns: new[] { "AuthorId", "Title" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_AuthorId_Title",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AuthorId",
                table: "Categories",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Id",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true);
        }
    }
}
