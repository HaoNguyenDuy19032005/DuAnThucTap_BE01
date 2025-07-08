using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAnThucTapNhom3.Migrations
{
    public partial class Demo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_Schoolyearid",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_SchoolYearlyStatuses_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.RenameColumn(
                name: "Schoolyearid",
                table: "SchoolYearlyStatuses",
                newName: "SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_Schoolyearid",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_SchoolYearId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId",
                principalTable: "schoolyears",
                principalColumn: "schoolyearid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropColumn(
                name: "SchoolYearlyStatusId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "SchoolYearlyStatusId",
                table: "schoolyears");

            migrationBuilder.RenameColumn(
                name: "SchoolYearId",
                table: "SchoolYearlyStatuses",
                newName: "Schoolyearid");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_SchoolYearId",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_Schoolyearid");

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearId",
                table: "SchoolYearlyStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_Schoolyearid",
                table: "SchoolYearlyStatuses",
                column: "Schoolyearid",
                principalTable: "schoolyears",
                principalColumn: "schoolyearid");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId",
                principalTable: "schoolyears",
                principalColumn: "schoolyearid");
        }
    }
}
