using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class AddAddressesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dm_addresses",
                schema: "dbo",
                columns: table => new
                {
                    ref_addresses_people_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ref_address_id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    person_id = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    is_contact_address = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    is_display_address = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    address = table.Column<string>(type: "character varying(464)", maxLength: 464, nullable: true),
                    post_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    unique_id = table.Column<long>(type: "bigint", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dm_addresses", x => x.ref_addresses_people_id);
                    table.ForeignKey(
                        name: "FK_dm_addresses_dm_persons_person_id",
                        column: x => x.person_id,
                        principalSchema: "dbo",
                        principalTable: "dm_persons",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dm_addresses_person_id",
                schema: "dbo",
                table: "dm_addresses",
                column: "person_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dm_addresses",
                schema: "dbo");
        }
    }
}
