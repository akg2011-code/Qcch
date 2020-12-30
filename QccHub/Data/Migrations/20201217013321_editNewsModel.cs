using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class editNewsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_CompanyID",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_CompanyID",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "News");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_News_CompanyID",
                table: "News",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_CompanyID",
                table: "News",
                column: "CompanyID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
