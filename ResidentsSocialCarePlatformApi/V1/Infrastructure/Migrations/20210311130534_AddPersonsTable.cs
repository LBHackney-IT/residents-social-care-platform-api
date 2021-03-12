using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddPersonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "dm_persons",
                schema: "dbo",
                columns: table => new
                {
                    person_id = table.Column<long>(type: "bigint", maxLength: 16, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: true),
                    title = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    first_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    last_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    nhs_id = table.Column<long>(type: "bigint", maxLength: 10, nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    context_flag = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    gender = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    nationality = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    email_address = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    person_id_legacy = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    restricted = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dm_persons", x => x.person_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dm_persons",
                schema: "dbo");
        }
    }
}
