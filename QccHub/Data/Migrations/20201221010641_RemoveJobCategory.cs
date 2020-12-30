using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class RemoveJobCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobCategory_JobCategoryID",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobCategoryID",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobCategoryID",
                table: "Job");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobCategoryID",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategoryID",
                table: "Job",
                column: "JobCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobCategory_JobCategoryID",
                table: "Job",
                column: "JobCategoryID",
                principalTable: "JobCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
