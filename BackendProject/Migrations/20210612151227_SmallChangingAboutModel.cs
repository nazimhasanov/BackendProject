using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProject.Migrations
{
    public partial class SmallChangingAboutModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstSubtitle",
                table: "About");

            migrationBuilder.DropColumn(
                name: "SecondSubtitle",
                table: "About");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "About",
                newName: "Subtitle");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "About",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "About");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "About",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "FirstSubtitle",
                table: "About",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondSubtitle",
                table: "About",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
