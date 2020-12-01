using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Infrastructure.Migrations
{
    public partial class ValueObjectDefined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxOffice",
                table: "Companies",
                newName: "TaxInfo_TaxOffice");

            migrationBuilder.RenameColumn(
                name: "TaxNumber",
                table: "Companies",
                newName: "TaxInfo_TaxNumber");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "Companies",
                newName: "Address_DistrictId");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Companies",
                newName: "Address_CountryId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Companies",
                newName: "Address_CityId");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Companies",
                newName: "Address_Address");

            migrationBuilder.AlterColumn<string>(
                name: "TaxInfo_TaxOffice",
                table: "Companies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "TaxInfo_TaxNumber",
                table: "Companies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address_CountryId",
                table: "Companies",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(24)",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<string>(
                name: "Address_CityId",
                table: "Companies",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(24)",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Address",
                table: "Companies",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxInfo_TaxOffice",
                table: "Companies",
                newName: "TaxOffice");

            migrationBuilder.RenameColumn(
                name: "TaxInfo_TaxNumber",
                table: "Companies",
                newName: "TaxNumber");

            migrationBuilder.RenameColumn(
                name: "Address_DistrictId",
                table: "Companies",
                newName: "DistrictId");

            migrationBuilder.RenameColumn(
                name: "Address_CountryId",
                table: "Companies",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "Address_CityId",
                table: "Companies",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "Address_Address",
                table: "Companies",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "TaxOffice",
                table: "Companies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "Companies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryId",
                table: "Companies",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CityId",
                table: "Companies",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Companies",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
