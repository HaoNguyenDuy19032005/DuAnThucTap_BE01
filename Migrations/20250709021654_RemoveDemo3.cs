using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAnThucTapNhom3.Migrations
{
    public partial class RemoveDemo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_Students_StudentId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_Teacherid",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_TeacherId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_Users_UserId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_SchoolYearlyStatuses_TeacherId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropColumn(
                name: "SchoolYearlyStatusId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "SchoolYearlyStatusId",
                table: "schoolyears");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.RenameColumn(
                name: "Teacherid",
                table: "SchoolYearlyStatuses",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_Teacherid",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_TeacherId");

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearlyStatusId",
                table: "classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId",
                principalTable: "schoolyears",
                principalColumn: "schoolyearid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_Students_StudentId",
                table: "SchoolYearlyStatuses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_TeacherId",
                table: "SchoolYearlyStatuses",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "teacherid",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_Users_UserId",
                table: "SchoolYearlyStatuses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_Students_StudentId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_TeacherId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_Users_UserId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropColumn(
                name: "SchoolYearlyStatusId",
                table: "classes");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "SchoolYearlyStatuses",
                newName: "Teacherid");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_TeacherId",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_Teacherid");

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearlyStatusId",
                table: "teachers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearlyStatusId",
                table: "schoolyears",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SchoolYearlyStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_TeacherId",
                table: "SchoolYearlyStatuses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId",
                principalTable: "schoolyears",
                principalColumn: "schoolyearid");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_Students_StudentId",
                table: "SchoolYearlyStatuses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_Teacherid",
                table: "SchoolYearlyStatuses",
                column: "Teacherid",
                principalTable: "teachers",
                principalColumn: "teacherid");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_TeacherId",
                table: "SchoolYearlyStatuses",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "teacherid",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_Users_UserId",
                table: "SchoolYearlyStatuses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
