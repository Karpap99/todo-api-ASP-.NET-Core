using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_api_backend.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Todos");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Todos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Todos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
