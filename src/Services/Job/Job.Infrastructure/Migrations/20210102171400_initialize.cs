using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Job.Infrastructure.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobAdvert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    SectorId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    JobPositionId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Title = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PersonCount = table.Column<short>(type: "smallint", nullable: true),
                    IsCanDisabilities = table.Column<bool>(type: "boolean", nullable: true),
                    MinExperienceYear = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0),
                    MaxExperienceYear = table.Column<byte>(type: "smallint", nullable: true),
                    Gender = table.Column<char>(type: "char(1)", nullable: false, defaultValue: 'U'),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ListingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstListingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ValidityDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdvert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobAdvertId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CvId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoverLetter = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Channel = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Referance = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplication_JobAdvert_JobAdvertId",
                        column: x => x.JobAdvertId,
                        principalTable: "JobAdvert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobEducationLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobAdvertId = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationLevelId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobEducationLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobEducationLevel_JobAdvert_JobAdvertId",
                        column: x => x.JobAdvertId,
                        principalTable: "JobAdvert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobAdvertId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    CityId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    DistrictId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobLocation_JobAdvert_JobAdvertId",
                        column: x => x.JobAdvertId,
                        principalTable: "JobAdvert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobViewingHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobAdvertId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Channel = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Referance = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobViewingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobViewingHistory_JobAdvert_JobAdvertId",
                        column: x => x.JobAdvertId,
                        principalTable: "JobAdvert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobWorkType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobAdvertId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkTypeId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorkType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobWorkType_JobAdvert_JobAdvertId",
                        column: x => x.JobAdvertId,
                        principalTable: "JobAdvert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobTag",
                columns: table => new
                {
                    JobAdvertId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTag", x => new { x.JobAdvertId, x.TagId });
                    table.ForeignKey(
                        name: "FK_JobTag_JobAdvert_JobAdvertId",
                        column: x => x.JobAdvertId,
                        principalTable: "JobAdvert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvert_CompanyId",
                table: "JobAdvert",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvert_CompanyId_IsDeleted",
                table: "JobAdvert",
                columns: new[] { "CompanyId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvert_CompanyId_IsPublished_IsDeleted",
                table: "JobAdvert",
                columns: new[] { "CompanyId", "IsPublished", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvert_JobPositionId",
                table: "JobAdvert",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvert_LanguageId",
                table: "JobAdvert",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvert_SectorId",
                table: "JobAdvert",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvert_Title",
                table: "JobAdvert",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobAdvertId",
                table: "JobApplication",
                column: "JobAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobAdvertId_IsDeleted",
                table: "JobApplication",
                columns: new[] { "JobAdvertId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_UserId_IsDeleted",
                table: "JobApplication",
                columns: new[] { "UserId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_JobEducationLevel_EducationLevelId",
                table: "JobEducationLevel",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobEducationLevel_JobAdvertId",
                table: "JobEducationLevel",
                column: "JobAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLocation_CityId",
                table: "JobLocation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLocation_CountryId",
                table: "JobLocation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLocation_DistrictId",
                table: "JobLocation",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLocation_JobAdvertId",
                table: "JobLocation",
                column: "JobAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTag_JobAdvertId",
                table: "JobTag",
                column: "JobAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTag_TagId",
                table: "JobTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_JobViewingHistory_JobAdvertId",
                table: "JobViewingHistory",
                column: "JobAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_JobWorkType_JobAdvertId",
                table: "JobWorkType",
                column: "JobAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                table: "Tag",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.DropTable(
                name: "JobEducationLevel");

            migrationBuilder.DropTable(
                name: "JobLocation");

            migrationBuilder.DropTable(
                name: "JobTag");

            migrationBuilder.DropTable(
                name: "JobViewingHistory");

            migrationBuilder.DropTable(
                name: "JobWorkType");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "JobAdvert");
        }
    }
}
