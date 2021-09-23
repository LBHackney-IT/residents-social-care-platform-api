using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class DropCopiedByColumnFromCaseNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "copied_by",
                schema: "dbo",
                table: "case_notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "copied_by",
                schema: "dbo",
                table: "case_notes",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
