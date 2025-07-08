using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DuAnThucTap.Migrations
{
    public partial class demo62 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classtypes",
                columns: table => new
                {
                    Classtypeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Classtypename = table.Column<string>(type: "text", nullable: false),
                    Isactive = table.Column<bool>(type: "boolean", nullable: true),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classtypes", x => x.Classtypeid);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Departmentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Departmentname = table.Column<string>(type: "text", nullable: false),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Departmentid);
                });

            migrationBuilder.CreateTable(
                name: "Gradetype",
                columns: table => new
                {
                    Gradetypeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Gradetypename = table.Column<string>(type: "text", nullable: false),
                    Weightingfactor = table.Column<decimal>(type: "numeric", nullable: false),
                    Mininstancessemester1 = table.Column<int>(type: "integer", nullable: false),
                    Mininstancessemester2 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradetype", x => x.Gradetypeid);
                });

            migrationBuilder.CreateTable(
                name: "Schoolinformations",
                columns: table => new
                {
                    Schoolinfoid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Schoolname = table.Column<string>(type: "text", nullable: false),
                    Standardcode = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Province = table.Column<string>(type: "text", nullable: true),
                    Ward = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    Schooltype = table.Column<string>(type: "text", nullable: true),
                    Phonenumber = table.Column<string>(type: "text", nullable: true),
                    Faxnumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Establishmentdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Trainingmodel = table.Column<string>(type: "text", nullable: true),
                    Websiteurl = table.Column<string>(type: "text", nullable: true),
                    Principalname = table.Column<string>(type: "text", nullable: true),
                    Principalphone = table.Column<string>(type: "text", nullable: true),
                    Logourl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schoolinformations", x => x.Schoolinfoid);
                });

            migrationBuilder.CreateTable(
                name: "Subjecttypes",
                columns: table => new
                {
                    Subjecttypeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subjecttypename = table.Column<string>(type: "text", nullable: false),
                    Isactive = table.Column<bool>(type: "boolean", nullable: true),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjecttypes", x => x.Subjecttypeid);
                });

            migrationBuilder.CreateTable(
                name: "Topiclists",
                columns: table => new
                {
                    Topicid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Topicname = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Teachingenddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topiclists", x => x.Topicid);
                });

            migrationBuilder.CreateTable(
                name: "Schoolyears",
                columns: table => new
                {
                    Schoolyearid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Schoolinfoid = table.Column<int>(type: "integer", nullable: false),
                    Schoolyearname = table.Column<string>(type: "text", nullable: false),
                    Startyear = table.Column<int>(type: "integer", nullable: false),
                    Endyear = table.Column<int>(type: "integer", nullable: false),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SchoolinformationSchoolinfoid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schoolyears", x => x.Schoolyearid);
                    table.ForeignKey(
                        name: "FK_Schoolyears_Schoolinformations_SchoolinformationSchoolinfoid",
                        column: x => x.SchoolinformationSchoolinfoid,
                        principalTable: "Schoolinformations",
                        principalColumn: "Schoolinfoid");
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Semesterid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Schoolyearid = table.Column<int>(type: "integer", nullable: false),
                    Semestername = table.Column<string>(type: "text", nullable: false),
                    Startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Semesterid);
                    table.ForeignKey(
                        name: "FK_Semesters_Schoolyears_Schoolyearid",
                        column: x => x.Schoolyearid,
                        principalTable: "Schoolyears",
                        principalColumn: "Schoolyearid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Subjectid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subjectname = table.Column<string>(type: "text", nullable: false),
                    Subjectcode = table.Column<string>(type: "text", nullable: true),
                    Defaultperiodssem1 = table.Column<int>(type: "integer", nullable: true),
                    Defaultperiodssem2 = table.Column<int>(type: "integer", nullable: true),
                    Departmentid = table.Column<int>(type: "integer", nullable: true),
                    Subjecttypeid = table.Column<int>(type: "integer", nullable: true),
                    Schoolyearid = table.Column<int>(type: "integer", nullable: true),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Subjectid);
                    table.ForeignKey(
                        name: "FK_Subjects_Departments_Departmentid",
                        column: x => x.Departmentid,
                        principalTable: "Departments",
                        principalColumn: "Departmentid");
                    table.ForeignKey(
                        name: "FK_Subjects_Schoolyears_Schoolyearid",
                        column: x => x.Schoolyearid,
                        principalTable: "Schoolyears",
                        principalColumn: "Schoolyearid");
                    table.ForeignKey(
                        name: "FK_Subjects_Subjecttypes_Subjecttypeid",
                        column: x => x.Subjecttypeid,
                        principalTable: "Subjecttypes",
                        principalColumn: "Subjecttypeid");
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Gradeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Studentid = table.Column<int>(type: "integer", nullable: false),
                    Subjectid = table.Column<int>(type: "integer", nullable: false),
                    Semesterid = table.Column<int>(type: "integer", nullable: false),
                    Gradetypeid = table.Column<int>(type: "integer", nullable: false),
                    Schoolid = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<decimal>(type: "numeric", nullable: false),
                    Instance = table.Column<int>(type: "integer", nullable: false),
                    Gradeddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Gradeid);
                    table.ForeignKey(
                        name: "FK_Grades_Gradetype_Gradetypeid",
                        column: x => x.Gradetypeid,
                        principalTable: "Gradetype",
                        principalColumn: "Gradetypeid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Schoolinformations_Schoolid",
                        column: x => x.Schoolid,
                        principalTable: "Schoolinformations",
                        principalColumn: "Schoolinfoid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Semesters_Semesterid",
                        column: x => x.Semesterid,
                        principalTable: "Semesters",
                        principalColumn: "Semesterid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Subjects_Subjectid",
                        column: x => x.Subjectid,
                        principalTable: "Subjects",
                        principalColumn: "Subjectid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Teacherid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Teachercode = table.Column<string>(type: "text", nullable: true),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Dateofbirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Ethnicity = table.Column<string>(type: "text", nullable: true),
                    Hiredate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    Religion = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Alias = table.Column<string>(type: "text", nullable: true),
                    AddressProvincecity = table.Column<string>(type: "text", nullable: true),
                    AddressWard = table.Column<string>(type: "text", nullable: true),
                    AddressDistrict = table.Column<string>(type: "text", nullable: true),
                    AddressStreet = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phonenumber = table.Column<string>(type: "text", nullable: true),
                    Dateofjoiningtheparty = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Dateofjoininggroup = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ispartymember = table.Column<bool>(type: "boolean", nullable: true),
                    Departmentid = table.Column<int>(type: "integer", nullable: true),
                    Subjectid = table.Column<int>(type: "integer", nullable: true),
                    Schoolyearid = table.Column<int>(type: "integer", nullable: true),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Teacherid);
                    table.ForeignKey(
                        name: "FK_Teachers_Departments_Departmentid",
                        column: x => x.Departmentid,
                        principalTable: "Departments",
                        principalColumn: "Departmentid");
                    table.ForeignKey(
                        name: "FK_Teachers_Schoolyears_Schoolyearid",
                        column: x => x.Schoolyearid,
                        principalTable: "Schoolyears",
                        principalColumn: "Schoolyearid");
                    table.ForeignKey(
                        name: "FK_Teachers_Subjects_Subjectid",
                        column: x => x.Subjectid,
                        principalTable: "Subjects",
                        principalColumn: "Subjectid");
                });

            migrationBuilder.CreateTable(
                name: "Departmentleaders",
                columns: table => new
                {
                    Departmentleaderid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Departmentid = table.Column<int>(type: "integer", nullable: false),
                    Schoolyearid = table.Column<int>(type: "integer", nullable: false),
                    Teacherid = table.Column<int>(type: "integer", nullable: false),
                    Startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmentleaders", x => x.Departmentleaderid);
                    table.ForeignKey(
                        name: "FK_Departmentleaders_Departments_Departmentid",
                        column: x => x.Departmentid,
                        principalTable: "Departments",
                        principalColumn: "Departmentid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Departmentleaders_Schoolyears_Schoolyearid",
                        column: x => x.Schoolyearid,
                        principalTable: "Schoolyears",
                        principalColumn: "Schoolyearid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Departmentleaders_Teachers_Teacherid",
                        column: x => x.Teacherid,
                        principalTable: "Teachers",
                        principalColumn: "Teacherid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gradelevels",
                columns: table => new
                {
                    Gradelevelid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Gradelevelname = table.Column<string>(type: "text", nullable: false),
                    Codegradelevel = table.Column<string>(type: "text", nullable: true),
                    Teacherid = table.Column<int>(type: "integer", nullable: true),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradelevels", x => x.Gradelevelid);
                    table.ForeignKey(
                        name: "FK_Gradelevels_Teachers_Teacherid",
                        column: x => x.Teacherid,
                        principalTable: "Teachers",
                        principalColumn: "Teacherid");
                });

            migrationBuilder.CreateTable(
                name: "Teachingassignments",
                columns: table => new
                {
                    Assignmentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Teacherid = table.Column<int>(type: "integer", nullable: false),
                    Subjectid = table.Column<int>(type: "integer", nullable: false),
                    Classtypeid = table.Column<int>(type: "integer", nullable: true),
                    Topicid = table.Column<int>(type: "integer", nullable: true),
                    Schoolyearid = table.Column<int>(type: "integer", nullable: false),
                    Teachingstartdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Teachingenddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachingassignments", x => x.Assignmentid);
                    table.ForeignKey(
                        name: "FK_Teachingassignments_Classtypes_Classtypeid",
                        column: x => x.Classtypeid,
                        principalTable: "Classtypes",
                        principalColumn: "Classtypeid");
                    table.ForeignKey(
                        name: "FK_Teachingassignments_Schoolyears_Schoolyearid",
                        column: x => x.Schoolyearid,
                        principalTable: "Schoolyears",
                        principalColumn: "Schoolyearid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachingassignments_Subjects_Subjectid",
                        column: x => x.Subjectid,
                        principalTable: "Subjects",
                        principalColumn: "Subjectid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachingassignments_Teachers_Teacherid",
                        column: x => x.Teacherid,
                        principalTable: "Teachers",
                        principalColumn: "Teacherid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachingassignments_Topiclists_Topicid",
                        column: x => x.Topicid,
                        principalTable: "Topiclists",
                        principalColumn: "Topicid");
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Classid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Classname = table.Column<string>(type: "text", nullable: false),
                    Maxstudents = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Schoolyearid = table.Column<int>(type: "integer", nullable: true),
                    Gradelevelid = table.Column<int>(type: "integer", nullable: true),
                    Classtypeid = table.Column<int>(type: "integer", nullable: true),
                    Teacherid = table.Column<int>(type: "integer", nullable: true),
                    Subjectid = table.Column<int>(type: "integer", nullable: true),
                    Createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Classid);
                    table.ForeignKey(
                        name: "FK_Classes_Classtypes_Classtypeid",
                        column: x => x.Classtypeid,
                        principalTable: "Classtypes",
                        principalColumn: "Classtypeid");
                    table.ForeignKey(
                        name: "FK_Classes_Gradelevels_Gradelevelid",
                        column: x => x.Gradelevelid,
                        principalTable: "Gradelevels",
                        principalColumn: "Gradelevelid");
                    table.ForeignKey(
                        name: "FK_Classes_Schoolyears_Schoolyearid",
                        column: x => x.Schoolyearid,
                        principalTable: "Schoolyears",
                        principalColumn: "Schoolyearid");
                    table.ForeignKey(
                        name: "FK_Classes_Subjects_Subjectid",
                        column: x => x.Subjectid,
                        principalTable: "Subjects",
                        principalColumn: "Subjectid");
                    table.ForeignKey(
                        name: "FK_Classes_Teachers_Teacherid",
                        column: x => x.Teacherid,
                        principalTable: "Teachers",
                        principalColumn: "Teacherid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Classtypeid",
                table: "Classes",
                column: "Classtypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Gradelevelid",
                table: "Classes",
                column: "Gradelevelid");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Schoolyearid",
                table: "Classes",
                column: "Schoolyearid");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Subjectid",
                table: "Classes",
                column: "Subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Teacherid",
                table: "Classes",
                column: "Teacherid");

            migrationBuilder.CreateIndex(
                name: "IX_Departmentleaders_Departmentid",
                table: "Departmentleaders",
                column: "Departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Departmentleaders_Schoolyearid",
                table: "Departmentleaders",
                column: "Schoolyearid");

            migrationBuilder.CreateIndex(
                name: "IX_Departmentleaders_Teacherid",
                table: "Departmentleaders",
                column: "Teacherid");

            migrationBuilder.CreateIndex(
                name: "IX_Gradelevels_Teacherid",
                table: "Gradelevels",
                column: "Teacherid");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Gradetypeid",
                table: "Grades",
                column: "Gradetypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Schoolid",
                table: "Grades",
                column: "Schoolid");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Semesterid",
                table: "Grades",
                column: "Semesterid");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Subjectid",
                table: "Grades",
                column: "Subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_Schoolyears_SchoolinformationSchoolinfoid",
                table: "Schoolyears",
                column: "SchoolinformationSchoolinfoid");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_Schoolyearid",
                table: "Semesters",
                column: "Schoolyearid");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Departmentid",
                table: "Subjects",
                column: "Departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Schoolyearid",
                table: "Subjects",
                column: "Schoolyearid");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Subjecttypeid",
                table: "Subjects",
                column: "Subjecttypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Departmentid",
                table: "Teachers",
                column: "Departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Schoolyearid",
                table: "Teachers",
                column: "Schoolyearid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Subjectid",
                table: "Teachers",
                column: "Subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachingassignments_Classtypeid",
                table: "Teachingassignments",
                column: "Classtypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachingassignments_Schoolyearid",
                table: "Teachingassignments",
                column: "Schoolyearid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachingassignments_Subjectid",
                table: "Teachingassignments",
                column: "Subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachingassignments_Teacherid",
                table: "Teachingassignments",
                column: "Teacherid");

            migrationBuilder.CreateIndex(
                name: "IX_Teachingassignments_Topicid",
                table: "Teachingassignments",
                column: "Topicid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Departmentleaders");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Teachingassignments");

            migrationBuilder.DropTable(
                name: "Gradelevels");

            migrationBuilder.DropTable(
                name: "Gradetype");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Classtypes");

            migrationBuilder.DropTable(
                name: "Topiclists");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Schoolyears");

            migrationBuilder.DropTable(
                name: "Subjecttypes");

            migrationBuilder.DropTable(
                name: "Schoolinformations");
        }
    }
}
