using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("schoolinformation")]
    [Index("Schoolinfoid", Name = "schoolinformation_schoolinfoid_key", IsUnique = true)]
    public partial class Schoolinformation
    {
        public Schoolinformation()
        {
            Campuses = new HashSet<Campus>();
            Schoolyears = new HashSet<Schoolyear>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("schoolinfoid")]
        [StringLength(255)]
        public string Schoolinfoid { get; set; } = null!;
        [Column("schoolname")]
        [StringLength(255)]
        public string Schoolname { get; set; } = null!;
        [Column("standardcode")]
        [StringLength(255)]
        public string? Standardcode { get; set; }
        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; } = null!;
        [Column("province")]
        [StringLength(255)]
        public string Province { get; set; } = null!;
        [Column("ward")]
        [StringLength(255)]
        public string? Ward { get; set; }
        [Column("district")]
        [StringLength(255)]
        public string District { get; set; } = null!;
        [Column("schooltype")]
        [StringLength(255)]
        public string Schooltype { get; set; } = null!;
        [Column("phonenumber")]
        [StringLength(20)]
        public string Phonenumber { get; set; } = null!;
        [Column("faxnumber")]
        [StringLength(20)]
        public string? Faxnumber { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("establishmentdate")]
        public DateOnly Establishmentdate { get; set; }
        [Column("trainingmodel")]
        [StringLength(255)]
        public string Trainingmodel { get; set; } = null!;
        [Column("websiteurl")]
        [StringLength(255)]
        public string? Websiteurl { get; set; }
        [Column("principalname")]
        [StringLength(255)]
        public string Principalname { get; set; } = null!;
        [Column("principalphone")]
        [StringLength(20)]
        public string Principalphone { get; set; } = null!;
        [Column("logourl")]
        [StringLength(255)]
        public string? Logourl { get; set; }

        public virtual ICollection<Campus> Campuses { get; set; }
        public virtual ICollection<Schoolyear> Schoolyears { get; set; }
    }
}
