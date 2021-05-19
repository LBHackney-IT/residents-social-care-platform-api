using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddPersonalRelationshipsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dm_personal_relationships",
                schema: "dbo",
                columns: table => new
                {
                    personal_relationship_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_id = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    personal_rel_type_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    other_person_id = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    family_category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_mother = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    parental_responsibility = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    is_informal_carer = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dm_personal_relationships", x => x.personal_relationship_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dm_personal_relationships",
                schema: "dbo");
        }
    }
}
