using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Infrastructure.Migrations
{
    public partial class addressrefrefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "Company",
                newName: "TaxCountryId");

            migrationBuilder.CreateTable(
                name: "CityRef",
                schema: "LK",
                columns: table => new
                {
                    RefId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityRef", x => x.RefId);
                });

            migrationBuilder.CreateTable(
                name: "CountryRef",
                schema: "LK",
                columns: table => new
                {
                    RefId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryRef", x => x.RefId);
                });

            migrationBuilder.CreateTable(
                name: "DistrictRef",
                schema: "LK",
                columns: table => new
                {
                    RefId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictRef", x => x.RefId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CountryRefRefId = table.Column<string>(type: "character varying(24)", nullable: false),
                    CityRefRefId = table.Column<string>(type: "character varying(24)", nullable: false),
                    DistrictRefRefId = table.Column<string>(type: "character varying(24)", nullable: true),
                    Details = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_CityRef_CityRefRefId",
                        column: x => x.CityRefRefId,
                        principalSchema: "LK",
                        principalTable: "CityRef",
                        principalColumn: "RefId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_CountryRef_CountryRefRefId",
                        column: x => x.CountryRefRefId,
                        principalSchema: "LK",
                        principalTable: "CountryRef",
                        principalColumn: "RefId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_DistrictRef_DistrictRefRefId",
                        column: x => x.DistrictRefRefId,
                        principalSchema: "LK",
                        principalTable: "DistrictRef",
                        principalColumn: "RefId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityRefRefId",
                table: "Address",
                column: "CityRefRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CompanyId",
                table: "Address",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryRefRefId",
                table: "Address",
                column: "CountryRefRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_DistrictRefRefId",
                table: "Address",
                column: "DistrictRefRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CityRef",
                schema: "LK");

            migrationBuilder.DropTable(
                name: "CountryRef",
                schema: "LK");

            migrationBuilder.DropTable(
                name: "DistrictRef",
                schema: "LK");

            migrationBuilder.RenameColumn(
                name: "TaxCountryId",
                table: "Company",
                newName: "DistrictId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Company",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "Company",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Company",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true);
        }
    }
}
