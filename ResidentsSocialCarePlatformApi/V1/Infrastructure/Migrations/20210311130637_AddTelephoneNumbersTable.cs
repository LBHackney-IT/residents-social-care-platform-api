using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddTelephoneNumbersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dm_telephone_numbers",
                schema: "dbo",
                columns: table => new
                {
                    telephone_number_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    telephone_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    telephone_number_type = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    person_id = table.Column<long>(type: "bigint", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dm_telephone_numbers", x => x.telephone_number_id);
                    table.ForeignKey(
                        name: "FK_dm_telephone_numbers_dm_persons_person_id",
                        column: x => x.person_id,
                        principalSchema: "dbo",
                        principalTable: "dm_persons",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dm_telephone_numbers_person_id",
                schema: "dbo",
                table: "dm_telephone_numbers",
                column: "person_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dm_telephone_numbers",
                schema: "dbo");
        }
    }
}
