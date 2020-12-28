using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class EditJobData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "JobCategory");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JobCategory",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsB2B",
                table: "Job",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "JobCategory");

            migrationBuilder.DropColumn(
                name: "IsB2B",
                table: "Job");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "JobCategory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
