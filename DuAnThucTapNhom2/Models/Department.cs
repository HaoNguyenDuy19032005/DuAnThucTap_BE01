using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("departments")]
    [Index("Departmentid", Name = "departments_departmentid_key", IsUnique = true)]
    public partial class Department
    {
        public Department()
        {
            Departmentleaders = new HashSet<Departmentleader>();
            Subjects = new HashSet<Subject>();
            Subjecttypes = new HashSet<Subjecttype>();
            Teachers = new HashSet<Teacher>();
            Teacherworkhistories = new HashSet<Teacherworkhistory>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("departmentid")]
        [StringLength(255)]
        public string Departmentid { get; set; } = null!;
        [Column("departmentname")]
        [StringLength(255)]
        public string Departmentname { get; set; } = null!;
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }

        public virtual ICollection<Departmentleader> Departmentleaders { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Subjecttype> Subjecttypes { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Teacherworkhistory> Teacherworkhistories { get; set; }
    }
}
