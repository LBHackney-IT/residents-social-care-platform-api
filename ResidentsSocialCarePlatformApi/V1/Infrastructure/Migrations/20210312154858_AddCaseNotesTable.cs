using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddCaseNotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "case_notes",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    person_visit_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    note_type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    effective_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    last_updated_by = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    root_case_note = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    completed_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    timeout_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    state = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    copy_of_case_note_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    copied_by = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    copied_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_notes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "case_notes",
                schema: "dbo");
        }
    }
}
