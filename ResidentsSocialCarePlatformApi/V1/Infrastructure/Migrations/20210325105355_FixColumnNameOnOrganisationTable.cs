using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    public partial class FixColumnNameOnOrganisationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "updated_on",
                schema: "dbo",
                table: "organisations",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated_on",
                schema: "dbo",
                table: "organisations");
        }
    }
}
