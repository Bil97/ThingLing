using Microsoft.EntityFrameworkCore.Migrations;

namespace ThingLing.Server.Migrations
{
    public partial class RemoveDisplayImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayImagePath",
                table: "Software");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayImagePath",
                table: "Software",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
