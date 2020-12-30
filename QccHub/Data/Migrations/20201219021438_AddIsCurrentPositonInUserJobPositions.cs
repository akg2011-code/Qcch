using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class AddIsCurrentPositonInUserJobPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentPosition",
                table: "JobPositions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentPosition",
                table: "UserJobPositions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentPosition",
                table: "UserJobPositions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentPosition",
                table: "JobPositions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
