using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_api_backend.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrations4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTaskCategories_Categories_CategoryId",
                table: "TodoTaskCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoTaskCategories_Todos_TodoId",
                table: "TodoTaskCategories");

            migrationBuilder.DropIndex(
                name: "IX_Todos_AuthorId",
                table: "Todos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTaskCategories",
                table: "TodoTaskCategories");

            migrationBuilder.RenameTable(
                name: "TodoTaskCategories",
                newName: "TodoCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTaskCategories_CategoryId",
                table: "TodoCategories",
                newName: "IX_TodoCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoCategories",
                table: "TodoCategories",
                columns: new[] { "TodoId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AuthorId_CreatedAt",
                table: "Todos",
                columns: new[] { "AuthorId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AuthorId_Title",
                table: "Todos",
                columns: new[] { "AuthorId", "Title" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Id",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoCategories_TodoId_CategoryId",
                table: "TodoCategories",
                columns: new[] { "TodoId", "CategoryId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoCategories_Categories_CategoryId",
                table: "TodoCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoCategories_Todos_TodoId",
                table: "TodoCategories",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoCategories_Categories_CategoryId",
                table: "TodoCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoCategories_Todos_TodoId",
                table: "TodoCategories");

            migrationBuilder.DropIndex(
                name: "IX_Todos_AuthorId_CreatedAt",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_AuthorId_Title",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Title",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoCategories",
                table: "TodoCategories");

            migrationBuilder.DropIndex(
                name: "IX_TodoCategories_TodoId_CategoryId",
                table: "TodoCategories");

            migrationBuilder.RenameTable(
                name: "TodoCategories",
                newName: "TodoTaskCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TodoCategories_CategoryId",
                table: "TodoTaskCategories",
                newName: "IX_TodoTaskCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTaskCategories",
                table: "TodoTaskCategories",
                columns: new[] { "TodoId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AuthorId",
                table: "Todos",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTaskCategories_Categories_CategoryId",
                table: "TodoTaskCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTaskCategories_Todos_TodoId",
                table: "TodoTaskCategories",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
