using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class editModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Category_CategoryID",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobPossition",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryID",
                table: "Job",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemsCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserJobPositions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobPossition = table.Column<string>(nullable: true),
                    YearsOfExperience = table.Column<int>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJobPositions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserJobPositions_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategoryID",
                table: "Job",
                column: "JobCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UserJobPositions_UserID",
                table: "UserJobPositions",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemsCategory_CategoryID",
                table: "Item",
                column: "CategoryID",
                principalTable: "ItemsCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobCategory_JobCategoryID",
                table: "Job",
                column: "JobCategoryID",
                principalTable: "JobCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemsCategory_CategoryID",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobCategory_JobCategoryID",
                table: "Job");

            migrationBuilder.DropTable(
                name: "ItemsCategory");

            migrationBuilder.DropTable(
                name: "JobCategory");

            migrationBuilder.DropTable(
                name: "UserJobPositions");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobCategoryID",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobCategoryID",
                table: "Job");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobPossition",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Category_CategoryID",
                table: "Item",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
