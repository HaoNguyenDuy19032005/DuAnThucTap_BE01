using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("departments")]
    public partial class Department
    {
        public Department()
        {
            Departmentleaders = new HashSet<Departmentleader>();
            Subjects = new HashSet<Subject>();
            Teachers = new HashSet<Teacher>();
            Teacherworkhistories = new HashSet<Teacherworkhistory>();
        }

        [Key]
        [Column("departmentid")]
        public int Departmentid { get; set; }
        [Column("departmentname")]
        [StringLength(255)]
        public string Departmentname { get; set; } = null!;
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Departmentleader> Departmentleaders { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Subject> Subjects { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Teacher> Teachers { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Teacherworkhistory> Teacherworkhistories { get; set; }
    }
}
