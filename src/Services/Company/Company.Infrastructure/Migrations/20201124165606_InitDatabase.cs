using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Infrastructure.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SectorId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CountryId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CityId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    DistrictId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TaxNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TaxOffice = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Website = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MobilePhone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FaxNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    EmployeesCount = table.Column<short>(type: "smallint", nullable: false),
                    EstablishedYear = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
