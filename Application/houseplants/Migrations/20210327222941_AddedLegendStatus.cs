using Microsoft.EntityFrameworkCore.Migrations;

namespace KrankSoft.HousePlants.Migrations
{
    public partial class AddedLegendStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LegendStatus",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegendStatus",
                table: "Plants");
        }
    }
}
