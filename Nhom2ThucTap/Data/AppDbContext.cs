using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcement> Announcements { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Classtransferhistory> Classtransferhistories { get; set; } = null!;
        public virtual DbSet<Classtype> Classtypes { get; set; } = null!;
        public virtual DbSet<Commendationtype> Commendationtypes { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Departmentleader> Departmentleaders { get; set; } = null!;
        public virtual DbSet<Disciplinetype> Disciplinetypes { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Examgrader> Examgraders { get; set; } = null!;
        public virtual DbSet<Examschedule> Examschedules { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Gradelevel> Gradelevels { get; set; } = null!;
        public virtual DbSet<Graderassignmenttype> Graderassignmenttypes { get; set; } = null!;
        public virtual DbSet<Gradetype> Gradetypes { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Operationunit> Operationunits { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Qnaparticipant> Qnaparticipants { get; set; } = null!;
        public virtual DbSet<Qnathread> Qnathreads { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Schoolinformation> Schoolinformations { get; set; } = null!;
        public virtual DbSet<Schooltransferhistory> Schooltransferhistories { get; set; } = null!;
        public virtual DbSet<Schoolyear> Schoolyears { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Studentcommendation> Studentcommendations { get; set; } = null!;
        public virtual DbSet<Studentdiscipline> Studentdisciplines { get; set; } = null!;
        public virtual DbSet<Studentexemption> Studentexemptions { get; set; } = null!;
        public virtual DbSet<Studentpreservation> Studentpreservations { get; set; } = null!;
        public virtual DbSet<Studentsemestersummary> Studentsemestersummaries { get; set; } = null!;
        public virtual DbSet<Studentsubjectsummary> Studentsubjectsummaries { get; set; } = null!;
        //public virtual DbSet<Studenttestanswer> Studenttestanswers { get; set; } = null!;
        ////public virtual DbSet<Studenttestsubmission> Studenttestsubmissions { get; set; } = null!;
        public virtual DbSet<Studentyearlystatus> Studentyearlystatuses { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Subjectsofexemption> Subjectsofexemptions { get; set; } = null!;
        public virtual DbSet<Subjecttype> Subjecttypes { get; set; } = null!;
        //public virtual DbSet<Submissionfile> Submissionfiles { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; } = null!;
        public virtual DbSet<Teachertraininghistory> Teachertraininghistories { get; set; } = null!;
        public virtual DbSet<Teacherworkhistory> Teacherworkhistories { get; set; } = null!;
        public virtual DbSet<Teacherworkstatushistory> Teacherworkstatushistories { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<Testassignment> Testassignments { get; set; } = null!;
        //public virtual DbSet<Testquestion> Testquestions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Usernotification> Usernotifications { get; set; } = null!;
        public virtual DbSet<Userthreadreadstatus> Userthreadreadstatuses { get; set; } = null!;
        public virtual DbSet<StudentTransferReceipt> StudentTransferReceipts { get; set; } = null!;
        public virtual DbSet<DisplayedTestList> DisplayedTestLists { get; set; } = null!;
        public virtual DbSet<TestHeader> TestHeaders { get; set; }
        public virtual DbSet<TestQuestionItem> TestQuestionItems { get; set; }
        public virtual DbSet<TestStudentSubmission> TestStudentSubmissions { get; set; }
        public virtual DbSet<TestStudentAnswer> TestStudentAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("grade", new[] { "A", "B", "C", "D", "F" })
                .HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("now()");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.Creatorid)
                    .HasConstraintName("fk_creator");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Classtype)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Classtypeid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_classtype");

                entity.HasOne(d => d.Gradelevel)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Gradelevelid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gradelevel");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyear");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Subjectid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_subject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Teacherid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Classtransferhistory>(entity =>
            {
                entity.HasKey(e => e.Transferid)
                    .HasName("classtransferhistory_pkey");

                entity.Property(e => e.Transferid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fromclass)
                    .WithMany(p => p.ClasstransferhistoryFromclasses)
                    .HasForeignKey(d => d.Fromclassid)
                    .HasConstraintName("fk_classes_from");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Classtransferhistories)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Classtransferhistories)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");

                entity.HasOne(d => d.Toclass)
                    .WithMany(p => p.ClasstransferhistoryToclasses)
                    .HasForeignKey(d => d.Toclassid)
                    .HasConstraintName("fk_classes_to");
            });

            modelBuilder.Entity<Classtype>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");
            });

            modelBuilder.Entity<Commendationtype>(entity =>
            {
                entity.Property(e => e.Commendationtypeid).UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.Teacherid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_contact_teacher");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Departmentleader>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Departmentleaders)
                    .HasForeignKey(d => d.Departmentid)
                    .HasConstraintName("fk_department");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Departmentleaders)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyear");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Departmentleaders)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Disciplinetype>(entity =>
            {
                entity.Property(e => e.Disciplinetypeid).UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("fk_class");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_student");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Classtype)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Classtypeid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_classtype");

                entity.HasOne(d => d.Gradelevel)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Gradelevelid)
                    .HasConstraintName("fk_gradelevel");

                entity.HasOne(d => d.Graderassignmenttype)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Graderassignmenttypeid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_grader_assignment_type");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyear");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semester");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Subjectid)
                    .HasConstraintName("fk_subject");
            });

            modelBuilder.Entity<Examgrader>(entity =>
            {
                entity.HasOne(d => d.Examschedule)
                    .WithMany(p => p.Examgraders)
                    .HasForeignKey(d => d.Examscheduleid)
                    .HasConstraintName("fk_exam_schedule");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Examgraders)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Examschedule>(entity =>
            {
                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Examschedules)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("fk_class");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Examschedules)
                    .HasForeignKey(d => d.Examid)
                    .HasConstraintName("fk_exam");
            });

            modelBuilder.Entity<StudentTransferReceipt>(entity =>
            {
                entity.ToTable("studenttransferreceipt");

                entity.HasKey(e => e.ReceiptId).HasName("pk_studenttransferreceipt");

                entity.Property(e => e.ReceiptId).HasColumnName("receiptid");
                entity.Property(e => e.StudentName).HasColumnName("studentname").IsRequired();
                entity.Property(e => e.StudentCode).HasColumnName("studentcode").HasMaxLength(50).IsRequired();
                entity.Property(e => e.TransferDate).HasColumnName("transferdate").HasColumnType("date").IsRequired();
                entity.Property(e => e.SemesterId).HasColumnName("semesterid").IsRequired();
                entity.Property(e => e.Province).HasColumnName("province").IsRequired();
                entity.Property(e => e.District).HasColumnName("district").IsRequired();
                entity.Property(e => e.FromSchool).HasColumnName("fromschool").IsRequired();
                entity.Property(e => e.Reason).HasColumnName("reason");
                entity.Property(e => e.AttachmentFile).HasColumnName("attachmentfile").IsRequired();

                entity.HasOne(e => e.Semester)
                      .WithMany(s => s.StudentTransferReceipts)
                      .HasForeignKey(e => e.SemesterId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_semester");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.Instance).HasDefaultValueSql("1");

                entity.HasOne(d => d.Gradetype)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.Gradetypeid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gradetype");

                entity.HasOne(d => d.Schoolinfo)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.Schoolinfoid)
                    .HasConstraintName("fk_school");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semester");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_student");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.Subjectid)
                    .HasConstraintName("fk_subject");
            });

            modelBuilder.Entity<Gradelevel>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Gradelevels)
                    .HasForeignKey(d => d.Teacherid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("now()");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.Courseid)
                    .HasConstraintName("fk_course");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Operationunit>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Qnaparticipant>(entity =>
            {
                entity.HasKey(e => new { e.Threadid, e.Userid })
                    .HasName("qnaparticipants_pkey");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Qnaparticipants)
                    .HasForeignKey(d => d.Threadid)
                    .HasConstraintName("fk_thread");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Qnaparticipants)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Qnathread>(entity =>
            {
                entity.HasKey(e => e.Threadid)
                    .HasName("qnathreads_pkey");

                entity.Property(e => e.Rowcreatedat).HasDefaultValueSql("now()");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Qnathreads)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("fk_class");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Qnathreads)
                    .HasForeignKey(d => d.Creatorid)
                    .HasConstraintName("fk_creator");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasMany(d => d.Permissions)
                    .WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "Rolepermission",
                        l => l.HasOne<Permission>().WithMany().HasForeignKey("Permissionid").HasConstraintName("fk_permission"),
                        r => r.HasOne<Role>().WithMany().HasForeignKey("Roleid").HasConstraintName("fk_role"),
                        j =>
                        {
                            j.HasKey("Roleid", "Permissionid").HasName("rolepermissions_pkey");

                            j.ToTable("rolepermissions");

                            j.IndexerProperty<int>("Roleid").HasColumnName("roleid");

                            j.IndexerProperty<int>("Permissionid").HasColumnName("permissionid");
                        });
            });

            modelBuilder.Entity<Schoolinformation>(entity =>
            {
                entity.HasKey(e => e.Schoolinfoid)
                    .HasName("schoolinformation_pkey");
            });

            modelBuilder.Entity<Schooltransferhistory>(entity =>
            {
                entity.HasKey(e => e.Schooltransferid)
                    .HasName("schooltransferhistory_pkey");

                entity.Property(e => e.Schooltransferid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Student)
                    .WithMany(p => p.Schooltransferhistories)
                    .HasForeignKey(e => e.Studentid)
                    .HasConstraintName("fk_students");

                entity.HasOne(e => e.Semester)
                    .WithMany(p => p.Schooltransferhistories)
                    .HasForeignKey(e => e.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(e => e.Fromclass)
                    .WithMany(p => p.SchooltransferhistoryFromclasses)
                    .HasForeignKey(e => e.Fromclassid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_classes_from");

                entity.HasOne(e => e.Toclass)
                    .WithMany(p => p.SchooltransferhistoryToclasses)
                    .HasForeignKey(e => e.Toclassid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_classes_to");

            });



            modelBuilder.Entity<Schoolyear>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Schoolinfo)
                    .WithMany(p => p.Schoolyears)
                    .HasForeignKey(d => d.Schoolinfoid)
                    .HasConstraintName("fk_schoolinfo");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyear");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Studentid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Createdat)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                entity.Property(e => e.Updatedat)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
            });


            modelBuilder.Entity<Studentcommendation>(entity =>
            {
                entity.HasKey(e => e.Commendationid)
                    .HasName("studentcommendations_pkey");

                entity.Property(e => e.Commendationid)
                    .UseIdentityAlwaysColumn(); // PostgreSQL identity column

                entity.Property(e => e.Createdat)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Commendationtype)
                    .WithMany(p => p.Studentcommendations)
                    .HasForeignKey(d => d.Commendationtypeid)
                    .HasConstraintName("fk_commendationtypes");

                entity.HasOne(d => d.Schoolinfo)
                    .WithMany(p => p.Studentcommendations)
                    .HasForeignKey(d => d.Schoolinfoid)
                    .HasConstraintName("fk_schools");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Studentcommendations)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentcommendations)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Studentdiscipline>(entity =>
            {
                entity.HasKey(e => e.Disciplineid)
                    .HasName("studentdisciplines_pkey");

                entity.Property(e => e.Disciplineid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Disciplinetype)
                    .WithMany(p => p.Studentdisciplines)
                    .HasForeignKey(d => d.Disciplinetypeid)
                    .HasConstraintName("fk_disciplinetypes");

                entity.HasOne(d => d.Schoolinfo)
                    .WithMany(p => p.Studentdisciplines)
                    .HasForeignKey(d => d.Schoolinfoid)
                    .HasConstraintName("fk_schools");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Studentdisciplines)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentdisciplines)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Studentexemption>(entity =>
            {
                entity.Property(e => e.Studentexemptionid).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.Studentexemptions)
                    .HasForeignKey(d => d.Objectid)
                    .HasConstraintName("fk_subjectsofexemption");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentexemptions)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Studentpreservation>(entity =>
            {
                entity.HasKey(e => e.Preservationid)
                    .HasName("studentpreservations_pkey");

                entity.Property(e => e.Preservationid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Studentpreservations)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("fk_classes");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Studentpreservations)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentpreservations)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Studentsemestersummary>(entity =>
            {
                entity.HasKey(e => e.Summaryid)
                    .HasName("studentsemestersummary_pkey");

                entity.Property(e => e.Summaryid).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Schoolinfo)
                    .WithMany(p => p.Studentsemestersummaries)
                    .HasForeignKey(d => d.Schoolinfoid)
                    .HasConstraintName("fk_schools");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Studentsemestersummaries)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentsemestersummaries)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Studentsubjectsummary>(entity =>
            {
                entity.HasKey(e => e.Subjectsummaryid)
                    .HasName("studentsubjectsummary_pkey");

                entity.Property(e => e.Subjectsummaryid).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Schoolinfo)
                    .WithMany(p => p.Studentsubjectsummaries)
                    .HasForeignKey(d => d.Schoolinfoid)
                    .HasConstraintName("fk_schools");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Studentsubjectsummaries)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentsubjectsummaries)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Studentsubjectsummaries)
                    .HasForeignKey(d => d.Subjectid)
                    .HasConstraintName("fk_subjects");
            });

            //modelBuilder.Entity<Studenttestanswer>(entity =>
            //{
            //    entity.HasKey(e => e.Answerid)
            //        .HasName("studenttestanswers_pkey");

            //    entity.HasOne(d => d.Question)
            //        .WithMany(p => p.Studenttestanswers)
            //        .HasForeignKey(d => d.Questionid)
            //        .HasConstraintName("fk_question");

            //    entity.HasOne(d => d.Submission)
            //        .WithMany(p => p.Studenttestanswers)
            //        .HasForeignKey(d => d.Submissionid)
            //        .HasConstraintName("fk_submission");
            //});

            //modelBuilder.Entity<Studenttestsubmission>(entity =>
            //{
            //    entity.HasKey(e => e.Submissionid)
            //        .HasName("studenttestsubmissions_pkey");

            //    entity.HasOne(d => d.Assignment)
            //        .WithMany(p => p.Studenttestsubmissions)
            //        .HasForeignKey(d => d.Assignmentid)
            //        .HasConstraintName("fk_assignment");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.Studenttestsubmissions)
            //        .HasForeignKey(d => d.Studentid)
            //        .HasConstraintName("fk_student");
            //});

            modelBuilder.Entity<Studentyearlystatus>(entity =>
            {
                entity.Property(e => e.Studentyearlystatusid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Studentyearlystatuses)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("fk_classes");

                entity.HasOne(d => d.Gradelevel)
                    .WithMany(p => p.Studentyearlystatuses)
                    .HasForeignKey(d => d.Gradelevelid)
                    .HasConstraintName("fk_gradelevel");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Studentyearlystatuses)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyears");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentyearlystatuses)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.Departmentid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_department");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyear");

                entity.HasOne(d => d.Subjecttype)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.Subjecttypeid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_subjecttype");
            });

            modelBuilder.Entity<Subjectsofexemption>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("subjectsofexemption_pkey");

                entity.Property(e => e.Objectid).UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<Subjecttype>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isactive).HasDefaultValueSql("true");
            });

            //modelBuilder.Entity<Submissionfile>(entity =>
            //{
            //    entity.HasKey(e => e.Fileid)
            //        .HasName("submissionfiles_pkey");

            //    entity.HasOne(d => d.Submission)
            //        .WithMany(p => p.Submissionfiles)
            //        .HasForeignKey(d => d.Submissionid)
            //        .HasConstraintName("fk_submission");
            //});

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.Departmentid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_teacher_department");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.Schoolyearid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_teacher_schoolyear");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.Subjectid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_teacher_subject");
            });

            modelBuilder.Entity<Teacherconcurrentsubject>(entity =>
            {
                entity.HasKey(e => new { e.Teacherid, e.Subjectid, e.Schoolyearid })
                    .HasName("teacherconcurrentsubjects_pkey");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Teacherconcurrentsubjects)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyear");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Teacherconcurrentsubjects)
                    .HasForeignKey(d => d.Subjectid)
                    .HasConstraintName("fk_subject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Teacherconcurrentsubjects)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Teachertraininghistory>(entity =>
            {
                entity.HasKey(e => e.Trainingid)
                    .HasName("teachertraininghistory_pkey");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Teachertraininghistories)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Teacherworkhistory>(entity =>
            {
                entity.HasKey(e => e.Workhistoryid)
                    .HasName("teacherworkhistory_pkey");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Teacherworkhistories)
                    .HasForeignKey(d => d.Departmentid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_department");

                entity.HasOne(d => d.Operationunit)
                    .WithMany(p => p.Teacherworkhistories)
                    .HasForeignKey(d => d.Operationunitid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_operation_unit");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Teacherworkhistories)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Teacherworkstatushistory>(entity =>
            {
                entity.HasKey(e => e.Historyid)
                    .HasName("teacherworkstatushistory_pkey");

                entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Teacherworkstatushistories)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.Teacherid)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Testassignment>(entity =>
            {
                entity.HasKey(e => e.Assignmentid)
                    .HasName("testassignments_pkey");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Testassignments)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("fk_class");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Testassignments)
                    .HasForeignKey(d => d.Testid)
                    .HasConstraintName("fk_test");
            });

            //modelBuilder.Entity<Testquestion>(entity =>
            //{
            //    entity.HasKey(e => e.Questionid)
            //        .HasName("testquestions_pkey");

            //    entity.HasOne(d => d.Test)
            //        .WithMany(p => p.Testquestions)
            //        .HasForeignKey(d => d.Testid)
            //        .HasConstraintName("fk_test");
            //});

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Isactive).HasDefaultValueSql("true");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_role");

                entity.HasOne(d => d.Student)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.Studentid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_student");

                entity.HasOne(d => d.Teacher)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.Teacherid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_teacher");
            });

            modelBuilder.Entity<Usernotification>(entity =>
            {
                entity.HasKey(e => e.Uniqusernotificationdueid)
                    .HasName("usernotifications_pkey");

                entity.Property(e => e.Isread).HasDefaultValueSql("false");

                entity.HasOne(d => d.Announcement)
                    .WithMany(p => p.Usernotifications)
                    .HasForeignKey(d => d.Announcementid)
                    .HasConstraintName("fk_announcement");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usernotifications)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Userthreadreadstatus>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Threadid })
                    .HasName("userthreadreadstatus_pkey");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Userthreadreadstatuses)
                    .HasForeignKey(d => d.Threadid)
                    .HasConstraintName("fk_thread");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userthreadreadstatuses)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<DisplayedTestList>(entity =>
            {
                entity.ToTable("displayedtestlist");

                entity.HasKey(e => e.DisplayItemID)
                      .HasName("PK_DisplayedTestList");

                entity.Property(e => e.DisplayItemID)
                      .HasColumnName("displayitemid")
                      .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_displayed_subject_id");

                entity.HasOne(e => e.Teacher)
                      .WithMany()
                      .HasForeignKey(e => e.TeacherID)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_displayed_teacher_id");
            });

            modelBuilder.Entity<TestQuestionItem>()
    .HasOne(q => q.Test)
    .WithMany(t => t.TestQuestions)
    .HasForeignKey(q => q.TestId);

            // TestHeader - TestStudentSubmission
            modelBuilder.Entity<TestStudentSubmission>()
                .HasOne(s => s.Test)
                .WithMany(t => t.StudentSubmissions)
                .HasForeignKey(s => s.TestId);

            modelBuilder.Entity<TestStudentSubmission>()
                .HasOne(s => s.Student)
                .WithMany(s => s.StudentSubmissions)
                .HasForeignKey(s => s.StudentId)
                .HasPrincipalKey(s => s.Studentid);


            // TestStudentAnswer - TestStudentSubmission
            modelBuilder.Entity<TestStudentAnswer>()
                .HasOne(a => a.TestStudentSubmission)
                .WithMany(s => s.Answers)
                .HasForeignKey(a => a.SubmissionId);

            // TestStudentAnswer - TestQuestionItem
            modelBuilder.Entity<TestStudentAnswer>()
                .HasOne(a => a.TestQuestionItem)
                .WithMany(q => q.StudentAnswers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        

        OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
