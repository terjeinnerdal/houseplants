using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrnankSoft.HousePlants.Migrations
{
    public partial class Classification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Family_FamilyId",
                table: "Plants");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Genus_GenusId",
                table: "Plants");

            migrationBuilder.DropIndex(
                name: "IX_Plants_FamilyId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "Plants");

            migrationBuilder.RenameColumn(
                name: "GenusId",
                table: "Plants",
                newName: "ClassificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Plants_GenusId",
                table: "Plants",
                newName: "IX_Plants_ClassificationId");

            migrationBuilder.AlterColumn<short>(
                name: "WaterRequirement",
                table: "Plants",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<short>(
                name: "SoilRequirement",
                table: "Plants",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<short>(
                name: "LightRequirement",
                table: "Plants",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "LegendStatus",
                table: "Plants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MinimumTemperature",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Toxic",
                table: "Plants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WateringTechnique",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GenusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classification_Family_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classification_Genus_GenusId",
                        column: x => x.GenusId,
                        principalTable: "Genus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantGroup",
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
                    table.PrimaryKey("PK_PlantGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantPlantGroup",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantPlantGroup", x => new { x.GroupsId, x.PlantsId });
                    table.ForeignKey(
                        name: "FK_PlantPlantGroup_PlantGroup_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "PlantGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantPlantGroup_Plants_PlantsId",
                        column: x => x.PlantsId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classification_FamilyId",
                table: "Classification",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Classification_GenusId",
                table: "Classification",
                column: "GenusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantPlantGroup_PlantsId",
                table: "PlantPlantGroup",
                column: "PlantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Classification_ClassificationId",
                table: "Plants",
                column: "ClassificationId",
                principalTable: "Classification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Classification_ClassificationId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "PlantPlantGroup");

            migrationBuilder.DropTable(
                name: "PlantGroup");

            migrationBuilder.DropColumn(
                name: "MinimumTemperature",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Toxic",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "WateringTechnique",
                table: "Plants");

            migrationBuilder.RenameColumn(
                name: "ClassificationId",
                table: "Plants",
                newName: "GenusId");

            migrationBuilder.RenameIndex(
                name: "IX_Plants_ClassificationId",
                table: "Plants",
                newName: "IX_Plants_GenusId");

            migrationBuilder.AlterColumn<short>(
                name: "WaterRequirement",
                table: "Plants",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "SoilRequirement",
                table: "Plants",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "LightRequirement",
                table: "Plants",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LegendStatus",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyId",
                table: "Plants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FamilyId",
                table: "Plants",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Family_FamilyId",
                table: "Plants",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Genus_GenusId",
                table: "Plants",
                column: "GenusId",
                principalTable: "Genus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
