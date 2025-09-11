using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASO.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Add_tracker_in_generated_AI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at_utc",
                table: "generated_AI_content",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at_utc",
                table: "generated_AI_content",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at_utc",
                table: "generated_AI_content");

            migrationBuilder.DropColumn(
                name: "updated_at_utc",
                table: "generated_AI_content");
        }
    }
}
