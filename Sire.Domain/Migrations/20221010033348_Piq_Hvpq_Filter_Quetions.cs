using Microsoft.EntityFrameworkCore.Migrations;

namespace Sire.Domain.Migrations
{
    public partial class Piq_Hvpq_Filter_Quetions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hint",
                table: "Training_Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hint",
                table: "Training_Task");
        }
    }
}
