using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class UpdateVisitsCpRegistrationIdToBeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "cp_registration_id",
                schema: "dbo",
                table: "dm_visits",
                type: "bigint",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "cp_registration_id",
                schema: "dbo",
                table: "dm_visits",
                type: "bigint",
                maxLength: 9,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 9,
                oldNullable: true);
        }
    }
}
