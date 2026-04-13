using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_api_backend.Migrations
{
    /// <inheritdoc />
    public partial class AuthorCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AuthorId",
                table: "Categories",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_AuthorId",
                table: "Categories",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_AuthorId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AuthorId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Categories");
        }
    }
}
