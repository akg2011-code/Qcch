using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class editjobapplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ApplyJobs");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExpectedSalary",
                table: "ApplyJobs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentSalary",
                table: "ApplyJobs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "ApplyJobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "ApplyJobs");

            migrationBuilder.AlterColumn<int>(
                name: "ExpectedSalary",
                table: "ApplyJobs",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSalary",
                table: "ApplyJobs",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ApplyJobs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
