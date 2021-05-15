using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Infrastructure.Migrations
{
    public partial class SectorLookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SectorId",
                table: "Company",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(24)",
                oldMaxLength: 24);

            migrationBuilder.AddColumn<string>(
                name: "SectorName",
                table: "Company",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectorName",
                table: "Company");

            migrationBuilder.AlterColumn<string>(
                name: "SectorId",
                table: "Company",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(24)",
                oldMaxLength: 24,
                oldNullable: true);
        }
    }
}
