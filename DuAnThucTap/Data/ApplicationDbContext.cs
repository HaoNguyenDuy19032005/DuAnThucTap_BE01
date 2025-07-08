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

        // 👇 Add this part INSIDE the class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Schoolinformation>()
                .HasKey(s => s.Schoolinfoid);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Schoolinformation>()
                .HasKey(s => s.Schoolinfoid);

            modelBuilder.Entity<Teachingassignment>()
                .HasKey(t => t.Assignmentid);

            modelBuilder.Entity<Teachingassignment>()
     .HasOne(t => t.Topic)
     .WithMany(tl => tl.Teachingassignments)
     .HasForeignKey(t => t.Topicid);

        }

        // 👇 Add this part INSIDE the class
        public DbSet<DuAnThucTap.Model.Gradetype>? Gradetype { get; set; }
        

    }
}
