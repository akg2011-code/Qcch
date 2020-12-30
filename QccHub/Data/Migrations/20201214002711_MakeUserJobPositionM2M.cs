using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class MakeUserJobPositionM2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJobPositions_AspNetUsers_UserID",
                table: "UserJobPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserJobPositions",
                table: "UserJobPositions");

            migrationBuilder.DropIndex(
                name: "IX_UserJobPositions_UserID",
                table: "UserJobPositions");

            migrationBuilder.DropColumn(
                name: "IsCurrentPosition",
                table: "UserJobPositions");

            migrationBuilder.DropColumn(
                name: "JobPossition",
                table: "UserJobPositions");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserJobPositions");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "UserJobPositions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDate",
                table: "UserJobPositions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "UserJobPositions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "UserJobPositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "UserJobPositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobPositionId",
                table: "UserJobPositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserJobPositions",
                table: "UserJobPositions",
                columns: new[] { "CompanyId", "EmployeeId", "JobPositionId", "FromDate", "ToDate" });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    YearsOfExperience = table.Column<int>(nullable: true),
                    IsCurrentPosition = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserJobPositions_EmployeeId",
                table: "UserJobPositions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJobPositions_JobPositionId",
                table: "UserJobPositions",
                column: "JobPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobPositions_AspNetUsers_CompanyId",
                table: "UserJobPositions",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobPositions_AspNetUsers_EmployeeId",
                table: "UserJobPositions",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobPositions_JobPositions_JobPositionId",
                table: "UserJobPositions",
                column: "JobPositionId",
                principalTable: "JobPositions",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJobPositions_AspNetUsers_CompanyId",
                table: "UserJobPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobPositions_AspNetUsers_EmployeeId",
                table: "UserJobPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobPositions_JobPositions_JobPositionId",
                table: "UserJobPositions");

            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserJobPositions",
                table: "UserJobPositions");

            migrationBuilder.DropIndex(
                name: "IX_UserJobPositions_EmployeeId",
                table: "UserJobPositions");

            migrationBuilder.DropIndex(
                name: "IX_UserJobPositions_JobPositionId",
                table: "UserJobPositions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserJobPositions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "UserJobPositions");

            migrationBuilder.DropColumn(
                name: "JobPositionId",
                table: "UserJobPositions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDate",
                table: "UserJobPositions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "UserJobPositions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentPosition",
                table: "UserJobPositions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JobPossition",
                table: "UserJobPositions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "UserJobPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "UserJobPositions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserJobPositions",
                table: "UserJobPositions",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserJobPositions_UserID",
                table: "UserJobPositions",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobPositions_AspNetUsers_UserID",
                table: "UserJobPositions",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
