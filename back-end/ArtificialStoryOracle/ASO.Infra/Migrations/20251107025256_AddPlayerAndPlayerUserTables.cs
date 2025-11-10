using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASO.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerAndPlayerUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    keycloak_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    nickname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    last_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    keycloak_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    last_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    nickname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    type_player = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_player_users_email",
                table: "player_users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_player_users_keycloak_user_id",
                table: "player_users",
                column: "keycloak_user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_players_email",
                table: "players",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_players_keycloak_user_id",
                table: "players",
                column: "keycloak_user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_users");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
