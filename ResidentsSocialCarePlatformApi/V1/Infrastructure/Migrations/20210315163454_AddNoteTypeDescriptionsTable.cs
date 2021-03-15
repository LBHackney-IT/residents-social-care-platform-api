using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddNoteTypeDescriptionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dm_case_note_types",
                schema: "dbo",
                columns: table => new
                {
                    note_type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    note_type_description = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dm_case_note_types",
                schema: "dbo");
        }
    }
}
