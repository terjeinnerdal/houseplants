using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrnankSoft.HousePlants.Migrations
{
    public partial class BaseEntity_and_Plant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CommonName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AquiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LightRequirement = table.Column<short>(type: "smallint", nullable: false),
                    WaterRequirement = table.Column<short>(type: "smallint", nullable: false),
                    SoilRequirement = table.Column<short>(type: "smallint", nullable: false),
                    GenusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_Genus_GenusId",
                        column: x => x.GenusId,
                        principalTable: "Genus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_GenusId",
                table: "Plants",
                column: "GenusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Genus");
        }
    }
}
