using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("classes")]
    [Index("Classid", Name = "classes_classid_key", IsUnique = true)]
    public partial class Class
    {
        public Class()
        {
            ClasstransferhistoryFkToclasses = new HashSet<Classtransferhistory>();
            ClasstransferhistoryFromclasses = new HashSet<Classtransferhistory>();
            Enrollments = new HashSet<Enrollment>();
            Examschedules = new HashSet<Examschedule>();
            Files = new HashSet<File>();
            Notifications = new HashSet<Notification>();
            Schooltransferhistories = new HashSet<Schooltransferhistory>();
            Students = new HashSet<Student>();
            Testassignments = new HashSet<Testassignment>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("classid")]
        [StringLength(255)]
        public string Classid { get; set; } = null!;
        [Column("classname")]
        [StringLength(255)]
        public string Classname { get; set; } = null!;
        [Column("maxstudents")]
        public int Maxstudents { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;
        [Column("fk_gradelevelid")]
        [StringLength(255)]
        public string FkGradelevelid { get; set; } = null!;
        [Column("fk_classtypeid")]
        [StringLength(255)]
        public string FkClasstypeid { get; set; } = null!;
        [Column("fk_homeroomteacherid")]
        [StringLength(255)]
        public string FkHomeroomteacherid { get; set; } = null!;

        public virtual Classtype FkClasstype { get; set; } = null!;
        public virtual Gradelevel FkGradelevel { get; set; } = null!;
        public virtual Teacher FkHomeroomteacher { get; set; } = null!;
        public virtual Schoolyear FkSchoolyear { get; set; } = null!;
        public virtual ICollection<Classtransferhistory> ClasstransferhistoryFkToclasses { get; set; }
        public virtual ICollection<Classtransferhistory> ClasstransferhistoryFromclasses { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Examschedule> Examschedules { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Schooltransferhistory> Schooltransferhistories { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Testassignment> Testassignments { get; set; }
    }
}
