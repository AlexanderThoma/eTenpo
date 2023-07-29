using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTenpo.Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                table: "Products");
        }
    }
}
