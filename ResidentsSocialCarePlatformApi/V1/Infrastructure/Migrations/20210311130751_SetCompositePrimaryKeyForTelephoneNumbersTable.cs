using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class SetCompositePrimaryKeyForTelephoneNumbersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dm_telephone_numbers",
                schema: "dbo",
                table: "dm_telephone_numbers");

            migrationBuilder.AlterColumn<long>(
                name: "telephone_number_id",
                schema: "dbo",
                table: "dm_telephone_numbers",
                type: "bigint",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_dm_telephone_numbers",
                schema: "dbo",
                table: "dm_telephone_numbers",
                columns: new[] { "telephone_number_id", "person_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dm_telephone_numbers",
                schema: "dbo",
                table: "dm_telephone_numbers");

            migrationBuilder.AlterColumn<long>(
                name: "telephone_number_id",
                schema: "dbo",
                table: "dm_telephone_numbers",
                type: "bigint",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_dm_telephone_numbers",
                schema: "dbo",
                table: "dm_telephone_numbers",
                column: "telephone_number_id");
        }
    }
}
