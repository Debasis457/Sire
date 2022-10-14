using Microsoft.EntityFrameworkCore.Migrations;

namespace Sire.Domain.Migrations
{
    public partial class sectionc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "QuetionSubSection",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "QuetionSubSection");
        }
    }
}
