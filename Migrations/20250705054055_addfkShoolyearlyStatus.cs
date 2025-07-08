using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAnThucTapNhom3.Migrations
{
    public partial class addfkShoolyearlyStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_classes_ClassId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_gradelevels_GradelevelId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_semesters_SemesterId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_Students_StudentId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_TeacherId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "SchoolYearlyStatuses",
                newName: "Teacherid");

            migrationBuilder.RenameColumn(
                name: "SchoolYearId",
                table: "SchoolYearlyStatuses",
                newName: "Schoolyearid");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_TeacherId",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_Teacherid");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_SchoolYearId",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_Schoolyearid");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "SchoolYearlyStatuses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearId",
                table: "SchoolYearlyStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SchoolYearlyStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_TeacherId",
                table: "SchoolYearlyStatuses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_classes_ClassId",
                table: "SchoolYearlyStatuses",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "classid",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_gradelevels_GradelevelId",
                table: "SchoolYearlyStatuses",
                column: "GradelevelId",
                principalTable: "gradelevels",
                principalColumn: "gradelevelid",
                onDelete: ReferentialAction.SetNull);

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

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_semesters_SemesterId",
                table: "SchoolYearlyStatuses",
                column: "SemesterId",
                principalTable: "semesters",
                principalColumn: "semesterid",
                onDelete: ReferentialAction.SetNull);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_classes_ClassId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_gradelevels_GradelevelId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_Schoolyearid",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolYearlyStatuses_semesters_SemesterId",
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

            migrationBuilder.DropIndex(
                name: "IX_SchoolYearlyStatuses_SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_SchoolYearlyStatuses_TeacherId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SchoolYearlyStatuses");

            migrationBuilder.RenameColumn(
                name: "Teacherid",
                table: "SchoolYearlyStatuses",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "Schoolyearid",
                table: "SchoolYearlyStatuses",
                newName: "SchoolYearId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_Teacherid",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolYearlyStatuses_Schoolyearid",
                table: "SchoolYearlyStatuses",
                newName: "IX_SchoolYearlyStatuses_SchoolYearId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "SchoolYearlyStatuses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_classes_ClassId",
                table: "SchoolYearlyStatuses",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "classid");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_gradelevels_GradelevelId",
                table: "SchoolYearlyStatuses",
                column: "GradelevelId",
                principalTable: "gradelevels",
                principalColumn: "gradelevelid");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId",
                principalTable: "schoolyears",
                principalColumn: "schoolyearid");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_semesters_SemesterId",
                table: "SchoolYearlyStatuses",
                column: "SemesterId",
                principalTable: "semesters",
                principalColumn: "semesterid");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_Students_StudentId",
                table: "SchoolYearlyStatuses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolYearlyStatuses_teachers_TeacherId",
                table: "SchoolYearlyStatuses",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "teacherid");
        }
    }
}
