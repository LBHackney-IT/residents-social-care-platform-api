using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddVisitsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dm_visits",
                schema: "dbo",
                columns: table => new
                {
                    visit_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_id = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    visit_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    planned_datetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    actual_datetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    reason_not_planned = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    reason_visit_not_made = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    seen_alone_flag = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    completed_flag = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    org_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: true),
                    worker_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: true),
                    cp_registration_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    cp_visit_schedule_step_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: true),
                    cp_visit_schedule_days = table.Column<long>(type: "bigint", maxLength: 3, nullable: true),
                    cp_visit_on_time = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dm_visits", x => x.visit_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dm_visits",
                schema: "dbo");
        }
    }
}
