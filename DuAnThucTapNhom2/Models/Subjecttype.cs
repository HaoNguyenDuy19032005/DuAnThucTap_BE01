using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("subjecttypes")]
    [Index("Subjecttypeid", Name = "subjecttypes_subjecttypeid_key", IsUnique = true)]
    public partial class Subjecttype
    {
        public Subjecttype()
        {
            Subjects = new HashSet<Subject>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("subjecttypeid")]
        [StringLength(255)]
        public string Subjecttypeid { get; set; } = null!;
        [Column("subjecttypename")]
        [StringLength(255)]
        public string Subjecttypename { get; set; } = null!;
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_departmentid")]
        [StringLength(255)]
        public string FkDepartmentid { get; set; } = null!;

        public virtual Department FkDepartment { get; set; } = null!;
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
