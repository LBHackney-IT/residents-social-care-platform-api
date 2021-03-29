using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddOrganisationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organisations",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    referrable = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    sector = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    sub_sector = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    approved_supplier = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    email_address = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    available = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    registering_authority = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    registration_status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    web_address = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    ac_flag = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    placement_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    team_org_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    created_acting_for = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    updated_acting_for = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    purchaser_flag = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    organisation_notes = table.Column<string>(type: "text", nullable: true),
                    department = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    ward_specific = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    responsible_authority = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organisations", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organisations",
                schema: "dbo");
        }
    }
}
