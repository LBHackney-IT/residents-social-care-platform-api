using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class MapWorkerToCaseNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completed_date",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "copied_date",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "copy_of_case_note_id",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "effective_date",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "last_updated_on",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "person_visit_id",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "root_case_note",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "state",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.DropColumn(
                name: "timeout_date",
                schema: "dbo",
                table: "case_notes");

            migrationBuilder.CreateIndex(
                name: "IX_dm_visits_worker_id",
                schema: "dbo",
                table: "dm_visits",
                column: "worker_id");

            migrationBuilder.AddForeignKey(
                name: "FK_dm_visits_workers_worker_id",
                schema: "dbo",
                table: "dm_visits",
                column: "worker_id",
                principalSchema: "dbo",
                principalTable: "workers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dm_visits_workers_worker_id",
                schema: "dbo",
                table: "dm_visits");

            migrationBuilder.DropIndex(
                name: "IX_dm_visits_worker_id",
                schema: "dbo",
                table: "dm_visits");

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "copied_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "copy_of_case_note_id",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "effective_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_updated_on",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "person_visit_id",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "root_case_note",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                schema: "dbo",
                table: "case_notes",
                type: "character varying(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "timeout_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
