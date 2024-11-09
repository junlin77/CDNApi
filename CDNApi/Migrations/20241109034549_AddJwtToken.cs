using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDNApi.Migrations
{
    /// <inheritdoc />
    public partial class AddJwtToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "ApiUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "ApiUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "ApiUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "ApiUsers");
        }
    }
}
