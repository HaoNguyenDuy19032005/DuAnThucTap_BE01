using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DuAnThucTapNhom2.Models;
using File = DuAnThucTapNhom2.Models.File; // Explicitly alias the 'File' class from your namespace


namespace DuAnThucTapNhom2.Data
{
    public partial class StudentsDbContext : DbContext
    {
        public StudentsDbContext()
        {
        }

        public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Campus> Campuses { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Classtransferhistory> Classtransferhistories { get; set; } = null!;
        public virtual DbSet<Classtype> Classtypes { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Departmentleader> Departmentleaders { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Examgrader> Examgraders { get; set; } = null!;
        public virtual DbSet<Examschedule> Examschedules { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Gradelevel> Gradelevels { get; set; } = null!;
        public virtual DbSet<Gradetype> Gradetypes { get; set; } = null!;
        public virtual DbSet<Incomingtransferhistory> Incomingtransferhistories { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Operationunit> Operationunits { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Rolepermission> Rolepermissions { get; set; } = null!;
        public virtual DbSet<Schoolinformation> Schoolinformations { get; set; } = null!;
        public virtual DbSet<Schooltransferhistory> Schooltransferhistories { get; set; } = null!;
        public virtual DbSet<Schoolyear> Schoolyears { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Studentcommendation> Studentcommendations { get; set; } = null!;
        public virtual DbSet<Studentdiscipline> Studentdisciplines { get; set; } = null!;
        public virtual DbSet<Studentexemption> Studentexemptions { get; set; } = null!;
        public virtual DbSet<Studentsemestersummary> Studentsemestersummaries { get; set; } = null!;
        public virtual DbSet<Studentsubjectsummary> Studentsubjectsummaries { get; set; } = null!;
        public virtual DbSet<Studenttestanswer> Studenttestanswers { get; set; } = null!;
        public virtual DbSet<Studenttestsubmission> Studenttestsubmissions { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Subjectsofexemption> Subjectsofexemptions { get; set; } = null!;
        public virtual DbSet<Subjecttype> Subjecttypes { get; set; } = null!;
        public virtual DbSet<Submissionfile> Submissionfiles { get; set; } = null!;
        public virtual DbSet<Systemsetting> Systemsettings { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; } = null!;
        public virtual DbSet<Teachertraininghistory> Teachertraininghistories { get; set; } = null!;
        public virtual DbSet<Teacherworkhistory> Teacherworkhistories { get; set; } = null!;
        public virtual DbSet<Teacherworkstatushistory> Teacherworkstatushistories { get; set; } = null!;
        public virtual DbSet<Teachingassignment> Teachingassignments { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<Testassignment> Testassignments { get; set; } = null!;
        public virtual DbSet<Testquestion> Testquestions { get; set; } = null!;
        public virtual DbSet<Theme> Themes { get; set; } = null!;
        public virtual DbSet<Topiclist> Topiclists { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=heThong;Username=postgres;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campus>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("campuses_pkey");

                entity.HasOne(d => d.FkSchoolinfo)
                    .WithMany(p => p.Campuses)
                    .HasPrincipalKey(p => p.Schoolinfoid)
                    .HasForeignKey(d => d.FkSchoolinfoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("campuses_fk_schoolinfoid_fkey");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("classes_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkClasstype)
                    .WithMany(p => p.Classes)
                    .HasPrincipalKey(p => p.Classtypeid)
                    .HasForeignKey(d => d.FkClasstypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classes_fk_classtypeid_fkey");

                entity.HasOne(d => d.FkGradelevel)
                    .WithMany(p => p.Classes)
                    .HasPrincipalKey(p => p.Gradelevelid)
                    .HasForeignKey(d => d.FkGradelevelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classes_fk_gradelevelid_fkey");

                entity.HasOne(d => d.FkHomeroomteacher)
                    .WithMany(p => p.Classes)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkHomeroomteacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classes_fk_homeroomteacherid_fkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Classes)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classes_fk_schoolyearid_fkey");
            });

            modelBuilder.Entity<Classtransferhistory>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("classtransferhistory_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkSemester)
                    .WithMany(p => p.Classtransferhistories)
                    .HasPrincipalKey(p => p.Semesterid)
                    .HasForeignKey(d => d.FkSemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtransferhistory_fk_semesterid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Classtransferhistories)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtransferhistory_fk_studentid_fkey");

                entity.HasOne(d => d.FkToclass)
                    .WithMany(p => p.ClasstransferhistoryFkToclasses)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.FkToclassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtransferhistory_fk_toclassid_fkey");

                entity.HasOne(d => d.Fromclass)
                    .WithMany(p => p.ClasstransferhistoryFromclasses)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.Fromclassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtransferhistory_fromclassid_fkey");
            });

            modelBuilder.Entity<Classtype>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("classtypes_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkGradelevel)
                    .WithMany(p => p.Classtypes)
                    .HasPrincipalKey(p => p.Gradelevelid)
                    .HasForeignKey(d => d.FkGradelevelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtypes_fk_gradelevelid_fkey");

                entity.HasOne(d => d.FkHomeroomteacher)
                    .WithMany(p => p.Classtypes)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkHomeroomteacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtypes_fk_homeroomteacherid_fkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Classtypes)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtypes_fk_schoolyearid_fkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Classtypes)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classtypes_fk_subjectid_fkey");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("contacts_pkey");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("courses_pkey");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher_id");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("departments_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Departmentleader>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("departmentleaders_pkey");

                entity.HasOne(d => d.FkDepartment)
                    .WithMany(p => p.Departmentleaders)
                    .HasPrincipalKey(p => p.Departmentid)
                    .HasForeignKey(d => d.FkDepartmentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("departmentleaders_fk_departmentid_fkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Departmentleaders)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("departmentleaders_fk_schoolyearid_fkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Departmentleaders)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("departmentleaders_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("enrollments_pkey");

                entity.Property(e => e.Enrollmentdate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Enrollments)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.FkClassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("enrollments_fk_classid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Enrollments)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("enrollments_fk_studentid_fkey");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("exams_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkGradelevel)
                    .WithMany(p => p.Exams)
                    .HasPrincipalKey(p => p.Gradelevelid)
                    .HasForeignKey(d => d.FkGradelevelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exams_fk_gradelevelid_fkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Exams)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exams_fk_schoolyearid_fkey");

                entity.HasOne(d => d.FkSemester)
                    .WithMany(p => p.Exams)
                    .HasPrincipalKey(p => p.Semesterid)
                    .HasForeignKey(d => d.FkSemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exams_fk_semesterid_fkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Exams)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exams_fk_subjectid_fkey");
            });

            modelBuilder.Entity<Examgrader>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("examgraders_pkey");

                entity.HasOne(d => d.FkExamschedule)
                    .WithMany(p => p.Examgraders)
                    .HasPrincipalKey(p => p.Examscheduleid)
                    .HasForeignKey(d => d.FkExamscheduleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("examgraders_fk_examscheduleid_fkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Examgraders)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("examgraders_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Examschedule>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("examschedules_pkey");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Examschedules)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.FkClassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("examschedules_fk_classid_fkey");

                entity.HasOne(d => d.FkExam)
                    .WithMany(p => p.Examschedules)
                    .HasPrincipalKey(p => p.Examid)
                    .HasForeignKey(d => d.FkExamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("examschedules_fk_examid_fkey");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("files_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Uploadtimestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Files)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.FkClassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("files_fk_classid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Files)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("files_fk_studentid_fkey");

                entity.HasOne(d => d.FkUploadedbyuser)
                    .WithMany(p => p.Files)
                    .HasPrincipalKey(p => p.Userid)
                    .HasForeignKey(d => d.FkUploadedbyuserid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("files_fk_uploadedbyuserid_fkey");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("grades_pkey");

                entity.HasOne(d => d.FkGradetype)
                    .WithMany(p => p.Grades)
                    .HasPrincipalKey(p => p.Gradetypeid)
                    .HasForeignKey(d => d.FkGradetypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grades_fk_gradetypeid_fkey");

                entity.HasOne(d => d.FkSemester)
                    .WithMany(p => p.Grades)
                    .HasPrincipalKey(p => p.Semesterid)
                    .HasForeignKey(d => d.FkSemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grades_fk_semesterid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Grades)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grades_fk_studentid_fkey");
            });

            modelBuilder.Entity<Gradelevel>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("gradelevels_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Gradelevels)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gradelevels_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Gradetype>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("gradetypes_pkey");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Incomingtransferhistory>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("incomingtransferhistory_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkSemester)
                    .WithMany(p => p.Incomingtransferhistories)
                    .HasPrincipalKey(p => p.Semesterid)
                    .HasForeignKey(d => d.FkSemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("incomingtransferhistory_fk_semesterid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Incomingtransferhistories)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("incomingtransferhistory_fk_studentid_fkey");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("lessons_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Lessons)
                    .HasPrincipalKey(p => p.Courseid)
                    .HasForeignKey(d => d.FkCourseid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lessons_fk_courseid_fkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Lessons)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lessons_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("notifications_pkey");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Notifications)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.FkClassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notifications_fk_classid_fkey");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Notifications)
                    .HasPrincipalKey(p => p.Userid)
                    .HasForeignKey(d => d.FkUserid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notifications_fk_userid_fkey");
            });

            modelBuilder.Entity<Operationunit>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("operationunit_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("permissions_pkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("roles_pkey");
            });

            modelBuilder.Entity<Rolepermission>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("rolepermissions_pkey");

                entity.HasOne(d => d.FkPermission)
                    .WithMany(p => p.Rolepermissions)
                    .HasPrincipalKey(p => p.Permissionid)
                    .HasForeignKey(d => d.FkPermissionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolepermissions_fk_permissionid_fkey");

                entity.HasOne(d => d.FkRole)
                    .WithMany(p => p.Rolepermissions)
                    .HasPrincipalKey(p => p.Roleid)
                    .HasForeignKey(d => d.FkRoleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolepermissions_fk_roleid_fkey");
            });

            modelBuilder.Entity<Schoolinformation>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("schoolinformation_pkey");
            });

            modelBuilder.Entity<Schooltransferhistory>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("schooltransferhistory_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Schooltransferhistories)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schooltransferhistory_fk_studentid_fkey");

                entity.HasOne(d => d.Fromclass)
                    .WithMany(p => p.Schooltransferhistories)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.Fromclassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schooltransferhistory_fromclassid_fkey");
            });

            modelBuilder.Entity<Schoolyear>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("schoolyears_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkSchoolinfo)
                    .WithMany(p => p.Schoolyears)
                    .HasPrincipalKey(p => p.Schoolinfoid)
                    .HasForeignKey(d => d.FkSchoolinfoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schoolyears_fk_schoolinfoid_fkey");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("semesters_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Semesters)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("semesters_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("students_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Students)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.FkClassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("students_fk_classid_fkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Students)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("students_fk_schoolyearid_fkey");

                entity.HasOne(d => d.Gradelevel)
                    .WithMany(p => p.Students)
                    .HasPrincipalKey(p => p.Gradelevelid)
                    .HasForeignKey(d => d.Gradelevelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("students_gradelevelid_fkey");
            });

            modelBuilder.Entity<Studentcommendation>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("studentcommendations_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Studentcommendations)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentcommendations_fk_studentid_fkey");
            });

            modelBuilder.Entity<Studentdiscipline>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("studentdisciplines_pkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Studentdisciplines)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentdisciplines_fk_studentid_fkey");
            });

            modelBuilder.Entity<Studentexemption>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("studentexemptions_pkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Studentexemptions)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentexemptions_fk_studentid_fkey");
            });

            modelBuilder.Entity<Studentsemestersummary>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("studentsemestersummary_pkey");

                entity.HasOne(d => d.FkSemester)
                    .WithMany(p => p.Studentsemestersummaries)
                    .HasPrincipalKey(p => p.Semesterid)
                    .HasForeignKey(d => d.FkSemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentsemestersummary_fk_semesterid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Studentsemestersummaries)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentsemestersummary_fk_studentid_fkey");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Studentsemestersummaries)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.Schoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentsemestersummary_schoolyearid_fkey");
            });

            modelBuilder.Entity<Studentsubjectsummary>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("studentsubjectsummary_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkSemester)
                    .WithMany(p => p.Studentsubjectsummaries)
                    .HasPrincipalKey(p => p.Semesterid)
                    .HasForeignKey(d => d.FkSemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentsubjectsummary_fk_semesterid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Studentsubjectsummaries)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentsubjectsummary_fk_studentid_fkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Studentsubjectsummaries)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentsubjectsummary_fk_subjectid_fkey");
            });

            modelBuilder.Entity<Studenttestanswer>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("studenttestanswers_pkey");

                entity.HasOne(d => d.FkSubmission)
                    .WithMany(p => p.Studenttestanswers)
                    .HasPrincipalKey(p => p.Submissionid)
                    .HasForeignKey(d => d.FkSubmissionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studenttestanswers_fk_submissionid_fkey");
            });

            modelBuilder.Entity<Studenttestsubmission>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("studenttestsubmissions_pkey");

                entity.HasOne(d => d.FkAssignment)
                    .WithMany(p => p.Studenttestsubmissions)
                    .HasPrincipalKey(p => p.Assignmentid)
                    .HasForeignKey(d => d.FkAssignmentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studenttestsubmissions_fk_assignmentid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Studenttestsubmissions)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studenttestsubmissions_fk_studentid_fkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Studenttestsubmissions)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studenttestsubmissions_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("subjects_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkDepartment)
                    .WithMany(p => p.Subjects)
                    .HasPrincipalKey(p => p.Departmentid)
                    .HasForeignKey(d => d.FkDepartmentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjects_fk_departmentid_fkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Subjects)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjects_fk_schoolyearid_fkey");

                entity.HasOne(d => d.FkSubjecttype)
                    .WithMany(p => p.Subjects)
                    .HasPrincipalKey(p => p.Subjecttypeid)
                    .HasForeignKey(d => d.FkSubjecttypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjects_fk_subjecttypeid_fkey");
            });

            modelBuilder.Entity<Subjectsofexemption>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("subjectsofexemption_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkSemester)
                    .WithMany(p => p.Subjectsofexemptions)
                    .HasPrincipalKey(p => p.Semesterid)
                    .HasForeignKey(d => d.FkSemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjectsofexemption_fk_semesterid_fkey");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Subjectsofexemptions)
                    .HasPrincipalKey(p => p.Studentid)
                    .HasForeignKey(d => d.FkStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjectsofexemption_fk_studentid_fkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Subjectsofexemptions)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjectsofexemption_fk_subjectid_fkey");
            });

            modelBuilder.Entity<Subjecttype>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("subjecttypes_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkDepartment)
                    .WithMany(p => p.Subjecttypes)
                    .HasPrincipalKey(p => p.Departmentid)
                    .HasForeignKey(d => d.FkDepartmentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subjecttypes_fk_departmentid_fkey");
            });

            modelBuilder.Entity<Submissionfile>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("submissionfiles_pkey");

                entity.HasOne(d => d.FkQuestion)
                    .WithOne(p => p.Submissionfile)
                    .HasPrincipalKey<Testquestion>(p => p.Questionid)
                    .HasForeignKey<Submissionfile>(d => d.FkQuestionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("submissionfiles_fk_questionid_fkey");
            });

            modelBuilder.Entity<Systemsetting>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("systemsettings_pkey");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Systemsettings)
                    .HasPrincipalKey(p => p.Userid)
                    .HasForeignKey(d => d.FkUserid)
                    .HasConstraintName("systemsettings_fk_userid_fkey");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("teachers_pkey");

                entity.HasOne(d => d.FkContact)
                    .WithMany(p => p.Teachers)
                    .HasPrincipalKey(p => p.Contactid)
                    .HasForeignKey(d => d.FkContactid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teachers_fk_contactid_fkey");

                entity.HasOne(d => d.FkDepartment)
                    .WithMany(p => p.Teachers)
                    .HasPrincipalKey(p => p.Departmentid)
                    .HasForeignKey(d => d.FkDepartmentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teachers_fk_departmentid_fkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Teachers)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teachers_fk_schoolyearid_fkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Teachers)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .HasConstraintName("fk_subject_id");
            });

            modelBuilder.Entity<Teacherconcurrentsubject>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("teacherconcurrentsubjects_pkey");

                entity.HasOne(d => d.FkSchoolyear)
                    .WithMany(p => p.Teacherconcurrentsubjects)
                    .HasPrincipalKey(p => p.Schoolyearid)
                    .HasForeignKey(d => d.FkSchoolyearid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacherconcurrentsubjects_fk_schoolyearid_fkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Teacherconcurrentsubjects)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacherconcurrentsubjects_fk_subjectid_fkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Teacherconcurrentsubjects)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacherconcurrentsubjects_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Teachertraininghistory>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("teachertraininghistory_pkey");

                entity.Property(e => e.Active).HasDefaultValueSql("true");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Teachertraininghistories)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teachertraininghistory_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Teacherworkhistory>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("teacherworkhistory_pkey");

                entity.HasOne(d => d.FkDepartment)
                    .WithMany(p => p.Teacherworkhistories)
                    .HasPrincipalKey(p => p.Departmentid)
                    .HasForeignKey(d => d.FkDepartmentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacherworkhistory_fk_departmentid_fkey");

                entity.HasOne(d => d.FkOperationunit)
                    .WithMany(p => p.Teacherworkhistories)
                    .HasPrincipalKey(p => p.Operationunitid)
                    .HasForeignKey(d => d.FkOperationunitid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacherworkhistory_fk_operationunitid_fkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Teacherworkhistories)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacherworkhistory_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Teacherworkstatushistory>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("teacherworkstatushistory_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Teacherworkstatushistories)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacherworkstatushistory_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Teachingassignment>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("teachingassignments_pkey");

                entity.HasOne(d => d.FkClasstype)
                    .WithMany(p => p.Teachingassignments)
                    .HasPrincipalKey(p => p.Classtypeid)
                    .HasForeignKey(d => d.FkClasstypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teachingassignments_fk_classtypeid_fkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Teachingassignments)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teachingassignments_fk_subjectid_fkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Teachingassignments)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teachingassignments_fk_teacherid_fkey");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("tests_pkey");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Tests)
                    .HasPrincipalKey(p => p.Teacherid)
                    .HasForeignKey(d => d.FkTeacherid)
                    .HasConstraintName("fk_teacher_id");
            });

            modelBuilder.Entity<Testassignment>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("testassignments_pkey");

                entity.Property(e => e.Sentat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Testassignments)
                    .HasPrincipalKey(p => p.Classid)
                    .HasForeignKey(d => d.FkClassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("testassignments_fk_classid_fkey");

                entity.HasOne(d => d.FkTest)
                    .WithMany(p => p.Testassignments)
                    .HasPrincipalKey(p => p.Testid)
                    .HasForeignKey(d => d.FkTestid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("testassignments_fk_testid_fkey");
            });

            modelBuilder.Entity<Testquestion>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("testquestions_pkey");

                entity.HasOne(d => d.FkTest)
                    .WithMany(p => p.Testquestions)
                    .HasPrincipalKey(p => p.Testid)
                    .HasForeignKey(d => d.FkTestid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("testquestions_fk_testid_fkey");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("themes_pkey");

                entity.HasOne(d => d.Thumbnail)
                    .WithMany(p => p.Themes)
                    .HasPrincipalKey(p => p.Fileid)
                    .HasForeignKey(d => d.Thumbnailid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("themes_thumbnailid_fkey");
            });

            modelBuilder.Entity<Topiclist>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("topiclist_pkey");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.Topiclists)
                    .HasPrincipalKey(p => p.Subjectid)
                    .HasForeignKey(d => d.FkSubjectid)
                    .HasConstraintName("topiclist_fk_subjectid_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("users_pkey");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasPrincipalKey(p => p.Roleid)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_roleid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
