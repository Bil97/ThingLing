using Microsoft.EntityFrameworkCore.Migrations;

namespace ThingLing.Server.Migrations
{
    public partial class DisplayImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "109fae4f-549c-44e6-933c-f82ed2e9a626");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "131e4f94-a92e-41c0-97c2-c3b4f6889e32");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Software",
                newName: "IconPath");

            migrationBuilder.AddColumn<string>(
                name: "DisplayImagePath",
                table: "ThreeDModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Software",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayImagePath",
                table: "Software",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca209aa4-76f8-4a72-adb9-e9cd2a5db4d5", "a5fa1a1e-4197-4fa9-acc2-1c85cee73804", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a741255-825a-4243-a0a3-eec752f1a5f7", "3c92540d-d124-45bc-a878-08ee411c5620", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a741255-825a-4243-a0a3-eec752f1a5f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca209aa4-76f8-4a72-adb9-e9cd2a5db4d5");

            migrationBuilder.DropColumn(
                name: "DisplayImagePath",
                table: "ThreeDModels");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Software");

            migrationBuilder.DropColumn(
                name: "DisplayImagePath",
                table: "Software");

            migrationBuilder.RenameColumn(
                name: "IconPath",
                table: "Software",
                newName: "Description");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "131e4f94-a92e-41c0-97c2-c3b4f6889e32", "95604b42-ea0f-42ef-b4b2-b2d5bb2e84aa", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "109fae4f-549c-44e6-933c-f82ed2e9a626", "ef31163b-4f93-46c9-9ebf-5482b538a2a5", "User", "USER" });
        }
    }
}
