using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrnankSoft.HousePlants.Migrations
{
    public partial class AddedFamily : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FamilyId",
                table: "Plants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Family",
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
                    table.PrimaryKey("PK_Family", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Family_FamilyId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropIndex(
                name: "IX_Plants_FamilyId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "Plants");
        }
    }
}
