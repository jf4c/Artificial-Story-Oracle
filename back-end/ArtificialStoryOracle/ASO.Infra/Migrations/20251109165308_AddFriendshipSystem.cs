using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASO.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendshipSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "friendships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    requester_id = table.Column<Guid>(type: "uuid", nullable: false),
                    addressee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    accepted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rejected_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendships", x => x.id);
                    table.ForeignKey(
                        name: "FK_friendships_players_addressee_id",
                        column: x => x.addressee_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friendships_players_requester_id",
                        column: x => x.requester_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendships_addressee_id",
                table: "friendships",
                column: "addressee_id");

            migrationBuilder.CreateIndex(
                name: "IX_friendships_requester_id",
                table: "friendships",
                column: "requester_id");

            migrationBuilder.CreateIndex(
                name: "IX_friendships_requester_id_addressee_id",
                table: "friendships",
                columns: new[] { "requester_id", "addressee_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_friendships_status",
                table: "friendships",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendships");
        }
    }
}
