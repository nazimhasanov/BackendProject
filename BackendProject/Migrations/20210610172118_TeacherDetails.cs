using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProject.Migrations
{
    public partial class TeacherDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TeacherDetails_TeacherDetailId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_TeacherDetailId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TeacherDetailId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "TeacherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Design",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Development",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TeacherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "TeacherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "TeacherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hobbies",
                table: "TeacherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Innovation",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TeacherDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TeacherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skype",
                table: "TeacherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamLeader",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDetails_TeacherId",
                table: "TeacherDetails",
                column: "TeacherId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherDetails_Teachers_TeacherId",
                table: "TeacherDetails",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherDetails_Teachers_TeacherId",
                table: "TeacherDetails");

            migrationBuilder.DropIndex(
                name: "IX_TeacherDetails_TeacherId",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Communication",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Design",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Development",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Hobbies",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Innovation",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "Skype",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "TeamLeader",
                table: "TeacherDetails");

            migrationBuilder.AddColumn<int>(
                name: "TeacherDetailId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TeacherDetailId",
                table: "Teachers",
                column: "TeacherDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_TeacherDetails_TeacherDetailId",
                table: "Teachers",
                column: "TeacherDetailId",
                principalTable: "TeacherDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
