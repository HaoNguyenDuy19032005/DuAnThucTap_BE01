using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DuAnThucTapNhom3.Migrations
{
    public partial class demo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "classtypes",
                columns: table => new
                {
                    classtypeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    classtypename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classtypes", x => x.classtypeid);
                });

            migrationBuilder.CreateTable(
                name: "gradelevels",
                columns: table => new
                {
                    gradelevelid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gradelevelname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    codegradelevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    headteacherid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gradelevels", x => x.gradelevelid);
                });

            migrationBuilder.CreateTable(
                name: "schoolyears",
                columns: table => new
                {
                    schoolyearid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    schoolyearname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    startyear = table.Column<int>(type: "integer", nullable: false),
                    endyear = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schoolyears", x => x.schoolyearid);
                });

            migrationBuilder.CreateTable(
                name: "subjecttypes",
                columns: table => new
                {
                    subjecttypeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subjecttypename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjecttypes", x => x.subjecttypeid);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    teacherid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fullname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.teacherid);
                });

            migrationBuilder.CreateTable(
                name: "semesters",
                columns: table => new
                {
                    semesterid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    schoolyearid = table.Column<int>(type: "integer", nullable: false),
                    semestername = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_semesters", x => x.semesterid);
                    table.ForeignKey(
                        name: "fk_schoolyear",
                        column: x => x.schoolyearid,
                        principalTable: "schoolyears",
                        principalColumn: "schoolyearid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    departmentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    departmentname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    headteacherid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.departmentid);
                    table.ForeignKey(
                        name: "fk_departments_teachers",
                        column: x => x.headteacherid,
                        principalTable: "teachers",
                        principalColumn: "teacherid");
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    subjectid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subjectname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    subjectcode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    defaultperiodssem1 = table.Column<int>(type: "integer", nullable: true),
                    defaultperiodssem2 = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    departmentid = table.Column<int>(type: "integer", nullable: true),
                    subjecttypeid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.subjectid);
                    table.ForeignKey(
                        name: "subjects_departmentid_fkey",
                        column: x => x.departmentid,
                        principalTable: "departments",
                        principalColumn: "departmentid");
                    table.ForeignKey(
                        name: "subjects_subjecttypeid_fkey",
                        column: x => x.subjecttypeid,
                        principalTable: "subjecttypes",
                        principalColumn: "subjecttypeid");
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    classid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    originalfilename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    storedfilepath = table.Column<string>(type: "text", nullable: false),
                    classname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    maxstudents = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    schoolyearid = table.Column<int>(type: "integer", nullable: true),
                    gradelevelid = table.Column<int>(type: "integer", nullable: true),
                    classtypeid = table.Column<int>(type: "integer", nullable: true),
                    homeroomteacherid = table.Column<int>(type: "integer", nullable: true),
                    subjectid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.classid);
                    table.ForeignKey(
                        name: "classes_classtypeid_fkey",
                        column: x => x.classtypeid,
                        principalTable: "classtypes",
                        principalColumn: "classtypeid");
                    table.ForeignKey(
                        name: "classes_gradelevelid_fkey",
                        column: x => x.gradelevelid,
                        principalTable: "gradelevels",
                        principalColumn: "gradelevelid");
                    table.ForeignKey(
                        name: "classes_subjectid_fkey",
                        column: x => x.subjectid,
                        principalTable: "subjects",
                        principalColumn: "subjectid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_classes_classtypeid",
                table: "classes",
                column: "classtypeid");

            migrationBuilder.CreateIndex(
                name: "IX_classes_gradelevelid",
                table: "classes",
                column: "gradelevelid");

            migrationBuilder.CreateIndex(
                name: "IX_classes_subjectid",
                table: "classes",
                column: "subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_departments_headteacherid",
                table: "departments",
                column: "headteacherid");

            migrationBuilder.CreateIndex(
                name: "gradelevels_codegradelevel_key",
                table: "gradelevels",
                column: "codegradelevel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "schoolyears_schoolyearname_key",
                table: "schoolyears",
                column: "schoolyearname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_semesters_schoolyearid",
                table: "semesters",
                column: "schoolyearid");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_departmentid",
                table: "subjects",
                column: "departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_subjecttypeid",
                table: "subjects",
                column: "subjecttypeid");

            migrationBuilder.CreateIndex(
                name: "subjects_subjectcode_key",
                table: "subjects",
                column: "subjectcode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "semesters");

            migrationBuilder.DropTable(
                name: "classtypes");

            migrationBuilder.DropTable(
                name: "gradelevels");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "schoolyears");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "subjecttypes");

            migrationBuilder.DropTable(
                name: "teachers");
        }
    }
}
