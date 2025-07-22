using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Classtype> Classtypes { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Gradelevel> Gradelevels { get; set; } = null!;
        public virtual DbSet<Schoolinformation> Schoolinformations { get; set; } = null!;
        public virtual DbSet<Schoolyear> Schoolyears { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Subjecttype> Subjecttypes { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Teachingassignment> Teachingassignments { get; set; } = null!;
        public virtual DbSet<Topiclist> Topiclists { get; set; } = null!;
        public virtual DbSet<Departmentleader> Departmentleaders { get; set; } = null!;
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<Blockleader> Blockleaders { get; set; } = null!;
        public DbSet<Campus> Campuses { get; set; } = null!;
        public DbSet<Gradetype> Gradetypes { get; set; } = null!;
        public DbSet<TeacherConcurrentSubject> TeacherConcurrentSubjects { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 👇 ClassSubject: many-to-many
            modelBuilder.Entity<ClassSubject>()
                .HasKey(cs => new { cs.Classid, cs.Subjectid });

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cs => cs.Classid);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.ClassSubjects)
                .HasForeignKey(cs => cs.Subjectid);

            // 👇 TeacherConcurrentSubject: many-to-many
            modelBuilder.Entity<TeacherConcurrentSubject>()
                .HasKey(tcs => new { tcs.TeacherID, tcs.SubjectID, tcs.SchoolYearID });

            modelBuilder.Entity<TeacherConcurrentSubject>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.TeacherID);

            modelBuilder.Entity<TeacherConcurrentSubject>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectID);

            modelBuilder.Entity<TeacherConcurrentSubject>()
                .HasOne(t => t.SchoolYear)
                .WithMany()
                .HasForeignKey(t => t.SchoolYearID);

            // 👇 Teachingassignment: 1-n
            modelBuilder.Entity<Teachingassignment>()
                .HasKey(t => t.Assignmentid);

            modelBuilder.Entity<Teachingassignment>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.Teacherid);

            modelBuilder.Entity<Teachingassignment>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.Subjectid);

            modelBuilder.Entity<Teachingassignment>()
                .HasOne(t => t.Classtype)
                .WithMany(c => c.Teachingassignments)
                .HasForeignKey(t => t.Classtypeid);

            modelBuilder.Entity<Teachingassignment>()
                .HasOne(t => t.Topic)
                .WithMany(tl => tl.Teachingassignments)
                .HasForeignKey(t => t.Topicid);

            modelBuilder.Entity<Teachingassignment>()
                .HasOne(t => t.Schoolyear)
                .WithMany()
                .HasForeignKey(t => t.Schoolyearid);

            // 👇 Class: 1-n
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Schoolyear)
                .WithMany(sy => sy.Classes)
                .HasForeignKey(c => c.Schoolyearid);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Gradelevel)
                .WithMany(gl => gl.Classes)
                .HasForeignKey(c => c.Gradelevelid);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Classtype)
                .WithMany(ct => ct.Classes)
                .HasForeignKey(c => c.Classtypeid);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.Teacherid);

            // 👇 Grade: 1-n
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Gradetype)
                .WithMany(gt => gt.Grades)
                .HasForeignKey(g => g.Gradetypeid);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Semester)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.Semesterid);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.Subjectid);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.School)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.Schoolid);

            // 👇 Subject
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Subjects)
                .HasForeignKey(s => s.Departmentid);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Schoolyear)
                .WithMany(sy => sy.Subjects)
                .HasForeignKey(s => s.Schoolyearid);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Subjecttype)
                .WithMany(st => st.Subjects)
                .HasForeignKey(s => s.Subjecttypeid);

            // 👇 Teacher
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.Departmentid);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Subject)
                .WithMany(s => s.Teachers)
                .HasForeignKey(t => t.Subjectid);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Schoolyear)
                .WithMany(sy => sy.Teachers)
                .HasForeignKey(t => t.Schoolyearid);

            // 👇 Blockleader
            modelBuilder.Entity<Blockleader>()
                .HasOne(b => b.Gradelevel)
                .WithMany(gl => gl.Blockleaders)
                .HasForeignKey(b => b.Gradelevelid);

            modelBuilder.Entity<Blockleader>()
                .HasOne(b => b.Schoolyear)
                .WithMany()
                .HasForeignKey(b => b.Schoolyearid);

            modelBuilder.Entity<Blockleader>()
                .HasOne(b => b.Teacher)
                .WithMany(t => t.Blockleaders)
                .HasForeignKey(b => b.Teacherid);

            // 👇 Departmentleader
            modelBuilder.Entity<Departmentleader>()
                .HasOne(dl => dl.Department)
                .WithMany()
                .HasForeignKey(dl => dl.Departmentid);

            modelBuilder.Entity<Departmentleader>()
                .HasOne(dl => dl.Teacher)
                .WithMany()
                .HasForeignKey(dl => dl.Teacherid);

            modelBuilder.Entity<Departmentleader>()
                .HasOne(dl => dl.Schoolyear)
                .WithMany()
                .HasForeignKey(dl => dl.Schoolyearid);

            // 👇 Schoolyear
            modelBuilder.Entity<Schoolyear>()
                .HasOne(sy => sy.Schoolinformation)
                .WithMany(si => si.Schoolyears)
                .HasForeignKey(sy => sy.Schoolinfoid);

            // 👇 Campus
            modelBuilder.Entity<Campus>()
                .HasOne(c => c.Schoolinformation)
                .WithMany(si => si.Campuses)
                .HasForeignKey(c => c.Schoolinfoid);

            // 👇 Semester
            modelBuilder.Entity<Semester>()
                .HasOne(s => s.Schoolyear)
                .WithMany(sy => sy.Semesters)
                .HasForeignKey(s => s.Schoolyearid);

            // 👇 Gradelevel
            modelBuilder.Entity<Gradelevel>()
                .HasOne(gl => gl.Teacher)
                .WithMany()
                .HasForeignKey(gl => gl.Teacherid);
        }

    }
}
