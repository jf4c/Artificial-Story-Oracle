using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASO.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ancestries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    backstory = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    size = table.Column<float>(type: "real", nullable: false),
                    displacement = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ancestries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    init_mana_points = table.Column<int>(type: "integer", nullable: false),
                    init_health_points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    key_attributes = table.Column<int>(type: "integer", nullable: false),
                    trained = table.Column<bool>(type: "boolean", nullable: false),
                    armor_penalty = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type_character = table.Column<int>(type: "integer", nullable: false),
                    AncestryId = table.Column<Guid>(type: "uuid", nullable: false),
                    mod_strength = table.Column<int>(type: "integer", nullable: false),
                    mod_dexterity = table.Column<int>(type: "integer", nullable: false),
                    mod_constitution = table.Column<int>(type: "integer", nullable: false),
                    mod_intelligence = table.Column<int>(type: "integer", nullable: false),
                    mod_wisdom = table.Column<int>(type: "integer", nullable: false),
                    mod_charisma = table.Column<int>(type: "integer", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    backstory = table.Column<string>(type: "text", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.id);
                    table.ForeignKey(
                        name: "FK_characters_ancestries_AncestryId",
                        column: x => x.AncestryId,
                        principalTable: "ancestries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_characters_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "character_skill",
                columns: table => new
                {
                    expertise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    character_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_skill", x => new { x.expertise_id, x.character_id });
                    table.ForeignKey(
                        name: "FK_character_skill_Skills_expertise_id",
                        column: x => x.expertise_id,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_skill_characters_character_id",
                        column: x => x.character_id,
                        principalTable: "characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "characters_classes",
                columns: table => new
                {
                    class_id = table.Column<Guid>(type: "uuid", nullable: false),
                    character_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters_classes", x => new { x.class_id, x.character_id });
                    table.ForeignKey(
                        name: "FK_characters_classes_characters_character_id",
                        column: x => x.character_id,
                        principalTable: "characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_characters_classes_classes_class_id",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_character_id",
                table: "character_skill",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "IX_characters_AncestryId",
                table: "characters",
                column: "AncestryId");

            migrationBuilder.CreateIndex(
                name: "IX_characters_ImageId",
                table: "characters",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_characters_classes_character_id",
                table: "characters_classes",
                column: "character_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_skill");

            migrationBuilder.DropTable(
                name: "characters_classes");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "ancestries");

            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
