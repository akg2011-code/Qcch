using Microsoft.EntityFrameworkCore.Migrations;

namespace QccHub.Data.Migrations
{
    public partial class EditInstituteName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_AspNetUsers_EmployeeId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_AspNetUsers_EmployeeId",
                table: "Education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificate",
                table: "Certificate");

            migrationBuilder.DropColumn(
                name: "Institue",
                table: "Education");

            migrationBuilder.RenameTable(
                name: "Certificate",
                newName: "Certificates");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_EmployeeId",
                table: "Certificates",
                newName: "IX_Certificates_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Education",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "Education",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Institute",
                table: "Education",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Certificates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "Certificates",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Education_EmployeeId1",
                table: "Education",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_EmployeeId1",
                table: "Certificates",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_AspNetUsers_EmployeeId",
                table: "Certificates",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_AspNetUsers_EmployeeId1",
                table: "Certificates",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_AspNetUsers_EmployeeId",
                table: "Education",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_AspNetUsers_EmployeeId1",
                table: "Education",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_AspNetUsers_EmployeeId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_AspNetUsers_EmployeeId1",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_AspNetUsers_EmployeeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_AspNetUsers_EmployeeId1",
                table: "Education");

            migrationBuilder.DropIndex(
                name: "IX_Education_EmployeeId1",
                table: "Education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_EmployeeId1",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "Institute",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Certificates");

            migrationBuilder.RenameTable(
                name: "Certificates",
                newName: "Certificate");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_EmployeeId",
                table: "Certificate",
                newName: "IX_Certificate_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Education",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Institue",
                table: "Education",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Certificate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificate",
                table: "Certificate",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_AspNetUsers_EmployeeId",
                table: "Certificate",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_AspNetUsers_EmployeeId",
                table: "Education",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
