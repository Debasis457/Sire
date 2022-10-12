using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sire.Domain.Migrations
{
    public partial class Piq_Hvpq_Filter_Quetions1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piq_Hvpq_Filter_Quetions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    VesselId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piq_Hvpq_Filter_Quetions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Piq_Hvpq_Filter_Quetions");
        }
    }
}
