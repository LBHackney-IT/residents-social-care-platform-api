using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class RemovePersonalRelationshipTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dm_personal_rel_types",
                schema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dm_personal_rel_types",
                schema: "dbo",
                columns: table => new
                {
                    personal_rel_type_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    family_category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_foster_and_adopt_sibling = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    is_informal_carer = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dm_personal_rel_types", x => x.personal_rel_type_id);
                });
        }
    }
}
