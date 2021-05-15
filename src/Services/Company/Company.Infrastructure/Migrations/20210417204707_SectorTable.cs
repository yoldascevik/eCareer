using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Infrastructure.Migrations
{
    public partial class SectorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SectorName",
                table: "Company");

            migrationBuilder.EnsureSchema(
                name: "LK");

            migrationBuilder.AddColumn<string>(
                name: "SectorRefId",
                table: "Company",
                type: "character varying(24)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SectorRef",
                schema: "LK",
                columns: table => new
                {
                    RefId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorRef", x => x.RefId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_SectorRefId",
                table: "Company",
                column: "SectorRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_SectorRef_SectorRefId",
                table: "Company",
                column: "SectorRefId",
                principalSchema: "LK",
                principalTable: "SectorRef",
                principalColumn: "RefId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_SectorRef_SectorRefId",
                table: "Company");

            migrationBuilder.DropTable(
                name: "SectorRef",
                schema: "LK");

            migrationBuilder.DropIndex(
                name: "IX_Company_SectorRefId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SectorRefId",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "SectorId",
                table: "Company",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectorName",
                table: "Company",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
