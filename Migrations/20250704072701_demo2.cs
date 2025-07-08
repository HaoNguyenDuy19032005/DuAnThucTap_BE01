using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DuAnThucTapNhom3.Migrations
{
    public partial class demo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentSemesterSummaries",
                columns: table => new
                {
                    SummaryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AverageScore = table.Column<double>(type: "double precision", nullable: false),
                    SemesterId = table.Column<int>(type: "integer", nullable: false),
                    Classid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSemesterSummaries", x => x.SummaryId);
                    table.ForeignKey(
                        name: "FK_StudentSemesterSummaries_classes_Classid",
                        column: x => x.Classid,
                        principalTable: "classes",
                        principalColumn: "classid");
                    table.ForeignKey(
                        name: "FK_StudentSemesterSummaries_semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "semesters",
                        principalColumn: "semesterid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    LoginCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentCode = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Classid = table.Column<int>(type: "integer", nullable: true),
                    StudentSemesterSummarysSummaryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_classes_Classid",
                        column: x => x.Classid,
                        principalTable: "classes",
                        principalColumn: "classid");
                    table.ForeignKey(
                        name: "FK_Students_StudentSemesterSummaries_StudentSemesterSummarysSu~",
                        column: x => x.StudentSemesterSummarysSummaryId,
                        principalTable: "StudentSemesterSummaries",
                        principalColumn: "SummaryId");
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    LoginLogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.LoginLogId);
                    table.ForeignKey(
                        name: "FK_LoginLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYearlyStatuses",
                columns: table => new
                {
                    IdSchoolYearlyStatus = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    SchoolYearId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ClassId = table.Column<int>(type: "integer", nullable: true),
                    GradelevelId = table.Column<int>(type: "integer", nullable: true),
                    SemesterId = table.Column<int>(type: "integer", nullable: true),
                    TeacherId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYearlyStatuses", x => x.IdSchoolYearlyStatus);
                    table.ForeignKey(
                        name: "FK_SchoolYearlyStatuses_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "classid");
                    table.ForeignKey(
                        name: "FK_SchoolYearlyStatuses_gradelevels_GradelevelId",
                        column: x => x.GradelevelId,
                        principalTable: "gradelevels",
                        principalColumn: "gradelevelid");
                    table.ForeignKey(
                        name: "FK_SchoolYearlyStatuses_schoolyears_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "schoolyears",
                        principalColumn: "schoolyearid");
                    table.ForeignKey(
                        name: "FK_SchoolYearlyStatuses_semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "semesters",
                        principalColumn: "semesterid");
                    table.ForeignKey(
                        name: "FK_SchoolYearlyStatuses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolYearlyStatuses_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "teacherid");
                    table.ForeignKey(
                        name: "FK_SchoolYearlyStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginLogs_UserId",
                table: "LoginLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_ClassId",
                table: "SchoolYearlyStatuses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_GradelevelId",
                table: "SchoolYearlyStatuses",
                column: "GradelevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_SchoolYearId",
                table: "SchoolYearlyStatuses",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_SemesterId",
                table: "SchoolYearlyStatuses",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_StudentId",
                table: "SchoolYearlyStatuses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_TeacherId",
                table: "SchoolYearlyStatuses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolYearlyStatuses_UserId",
                table: "SchoolYearlyStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Classid",
                table: "Students",
                column: "Classid");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentSemesterSummarysSummaryId",
                table: "Students",
                column: "StudentSemesterSummarysSummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemesterSummaries_Classid",
                table: "StudentSemesterSummaries",
                column: "Classid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemesterSummaries_SemesterId",
                table: "StudentSemesterSummaries",
                column: "SemesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "SchoolYearlyStatuses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StudentSemesterSummaries");
        }
    }
}
