using Microsoft.EntityFrameworkCore.Migrations;

namespace Memorial3.Migrations
{
    public partial class Remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Memorial");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Memorial",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
