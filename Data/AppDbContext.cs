using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Classtype> Classtypes { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Gradelevel> Gradelevels { get; set; } = null!;
        public virtual DbSet<Schoolyear> Schoolyears { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Subjecttype> Subjecttypes { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<StudentSemesterSummary> StudentSemesterSummaries { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<SchoolYearlyStatus> SchoolYearlyStatuses { get; set; } = null!;
        public virtual DbSet<LoginLog> LoginLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("classes");

                entity.Property(e => e.Classid).HasColumnName("classid");

                entity.Property(e => e.Classname)
                    .HasMaxLength(255)
                    .HasColumnName("classname");

                entity.Property(e => e.Classtypeid).HasColumnName("classtypeid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Gradelevelid).HasColumnName("gradelevelid");

                entity.Property(e => e.Homeroomteacherid).HasColumnName("homeroomteacherid");

                entity.Property(e => e.Maxstudents).HasColumnName("maxstudents");

                entity.Property(e => e.Originalfilename)
                    .HasMaxLength(255)
                    .HasColumnName("originalfilename");

                entity.Property(e => e.Schoolyearid).HasColumnName("schoolyearid");

                entity.Property(e => e.Storedfilepath).HasColumnName("storedfilepath");

                entity.Property(e => e.Subjectid).HasColumnName("subjectid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Classtype)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Classtypeid)
                    .HasConstraintName("classes_classtypeid_fkey");

                entity.HasOne(d => d.Gradelevel)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Gradelevelid)
                    .HasConstraintName("classes_gradelevelid_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Subjectid)
                    .HasConstraintName("classes_subjectid_fkey");
            });

            modelBuilder.Entity<Classtype>(entity =>
            {
                entity.ToTable("classtypes");

                entity.Property(e => e.Classtypeid).HasColumnName("classtypeid");

                entity.Property(e => e.Classtypename)
                    .HasMaxLength(255)
                    .HasColumnName("classtypename");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("departments");

                entity.Property(e => e.Departmentid).HasColumnName("departmentid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Departmentname)
                    .HasMaxLength(255)
                    .HasColumnName("departmentname");

                entity.Property(e => e.Headteacherid).HasColumnName("headteacherid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Headteacher)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.Headteacherid)
                    .HasConstraintName("fk_departments_teachers");
            });

            modelBuilder.Entity<Gradelevel>(entity =>
            {
                entity.ToTable("gradelevels");

                entity.HasIndex(e => e.Codegradelevel, "gradelevels_codegradelevel_key")
                    .IsUnique();

                entity.Property(e => e.Gradelevelid).HasColumnName("gradelevelid");

                entity.Property(e => e.Codegradelevel)
                    .HasMaxLength(50)
                    .HasColumnName("codegradelevel");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Gradelevelname)
                    .HasMaxLength(255)
                    .HasColumnName("gradelevelname");

                entity.Property(e => e.Headteacherid).HasColumnName("headteacherid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Schoolyear>(entity =>
            {
                entity.ToTable("schoolyears");

                entity.HasIndex(e => e.Schoolyearname, "schoolyears_schoolyearname_key")
                    .IsUnique();

                entity.Property(e => e.Schoolyearid).HasColumnName("schoolyearid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Endyear).HasColumnName("endyear");

                entity.Property(e => e.Schoolyearname)
                    .HasMaxLength(50)
                    .HasColumnName("schoolyearname");

                entity.Property(e => e.Startyear).HasColumnName("startyear");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("semesters");

                entity.HasIndex(e => e.Schoolyearid, "idx_semesters_schoolyearid");

                entity.Property(e => e.Semesterid).HasColumnName("semesterid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Enddate).HasColumnName("enddate");

                entity.Property(e => e.Schoolyearid).HasColumnName("schoolyearid");

                entity.Property(e => e.Semestername)
                    .HasMaxLength(100)
                    .HasColumnName("semestername");

                entity.Property(e => e.Startdate).HasColumnName("startdate");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Schoolyear)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => d.Schoolyearid)
                    .HasConstraintName("fk_schoolyear");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subjects");

                entity.HasIndex(e => e.Subjectcode, "subjects_subjectcode_key")
                    .IsUnique();

                entity.Property(e => e.Subjectid).HasColumnName("subjectid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Defaultperiodssem1).HasColumnName("defaultperiodssem1");

                entity.Property(e => e.Defaultperiodssem2).HasColumnName("defaultperiodssem2");

                entity.Property(e => e.Departmentid).HasColumnName("departmentid");

                entity.Property(e => e.Subjectcode)
                    .HasMaxLength(50)
                    .HasColumnName("subjectcode");

                entity.Property(e => e.Subjectname)
                    .HasMaxLength(255)
                    .HasColumnName("subjectname");

                entity.Property(e => e.Subjecttypeid).HasColumnName("subjecttypeid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.Departmentid)
                    .HasConstraintName("subjects_departmentid_fkey");

                entity.HasOne(d => d.Subjecttype)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.Subjecttypeid)
                    .HasConstraintName("subjects_subjecttypeid_fkey");
            });

            modelBuilder.Entity<Subjecttype>(entity =>
            {
                entity.ToTable("subjecttypes");

                entity.Property(e => e.Subjecttypeid).HasColumnName("subjecttypeid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Subjecttypename)
                    .HasMaxLength(255)
                    .HasColumnName("subjecttypename");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
          
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teachers");

                entity.Property(e => e.Teacherid).HasColumnName("teacherid");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .HasColumnName("fullname");
            });
            modelBuilder.Entity<SchoolYearlyStatus>(entity =>
            {
                entity.HasKey(e => e.IdSchoolYearlyStatus);

                entity.HasOne(e => e.SchoolYear)
                    .WithMany(y => y.SchoolYearlyStatuses)
                    .HasForeignKey(e => e.SchoolYearId)
                    .OnDelete(DeleteBehavior.Restrict); // hoặc bỏ qua

                entity.HasOne(e => e.Student)
                    .WithMany()
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Restrict); // tránh xóa mất status nếu xóa Student

                entity.HasOne(e => e.Teacher)
                    .WithMany(t => t.SchoolYearlyStatuses)
                    .HasForeignKey(e => e.TeacherId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Class)
                    .WithMany(c => c.SchoolYearlyStatuses)
                    .HasForeignKey(e => e.ClassId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Gradelevel)
                    .WithMany(g => g.SchoolYearlyStatuses)
                    .HasForeignKey(e => e.GradelevelId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Semester)
                    .WithMany(s => s.SchoolYearlyStatuses)
                    .HasForeignKey(e => e.SemesterId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.SchoolYearslyStatus)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // vì UserId là int (not null)
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
