using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Infrastructure.Migrations
{
    public partial class AlterColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
