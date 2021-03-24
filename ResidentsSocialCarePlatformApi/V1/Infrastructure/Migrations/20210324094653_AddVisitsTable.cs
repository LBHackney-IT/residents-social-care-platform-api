using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddVisitsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "note_type",
                schema: "dbo",
                table: "dm_case_note_types",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "timeout_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<long>(
                name: "root_case_note",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<long>(
                name: "person_visit_id",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_updated_on",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "effective_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<long>(
                name: "copy_of_case_note_id",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "copied_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "completed_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dm_case_note_types",
                schema: "dbo",
                table: "dm_case_note_types",
                column: "note_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dm_case_note_types",
                schema: "dbo",
                table: "dm_case_note_types");

            migrationBuilder.AlterColumn<string>(
                name: "note_type",
                schema: "dbo",
                table: "dm_case_note_types",
                type: "character varying(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<DateTime>(
                name: "timeout_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "root_case_note",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "person_visit_id",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_updated_on",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "effective_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "copy_of_case_note_id",
                schema: "dbo",
                table: "case_notes",
                type: "bigint",
                maxLength: 9,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "copied_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "completed_date",
                schema: "dbo",
                table: "case_notes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
