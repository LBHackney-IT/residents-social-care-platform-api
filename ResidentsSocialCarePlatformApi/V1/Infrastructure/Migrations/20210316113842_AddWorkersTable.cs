using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddWorkersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workers",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_names = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    last_names = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    system_user_id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    email_address = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    accessible = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    context_code = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "workers",
                schema: "dbo");
        }
    }
}
