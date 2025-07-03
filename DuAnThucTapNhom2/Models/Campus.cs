using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("campuses")]
    [Index("Campusid", Name = "campuses_campusid_key", IsUnique = true)]
    public partial class Campus
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("campusid")]
        [StringLength(255)]
        public string Campusid { get; set; } = null!;
        [Column("fk_schoolinfoid")]
        [StringLength(255)]
        public string FkSchoolinfoid { get; set; } = null!;
        [Column("campusname")]
        [StringLength(255)]
        public string Campusname { get; set; } = null!;
        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; } = null!;
        [Column("phonenumber")]
        [StringLength(255)]
        public string Phonenumber { get; set; } = null!;
        [Column("imageurl")]
        [StringLength(255)]
        public string Imageurl { get; set; } = null!;
        [Column("contactpersonname")]
        [StringLength(255)]
        public string Contactpersonname { get; set; } = null!;
        [Column("contactpersonmobile")]
        [StringLength(255)]
        public string Contactpersonmobile { get; set; } = null!;
        [Column("contactpersonemail")]
        [StringLength(255)]
        public string Contactpersonemail { get; set; } = null!;

        public virtual Schoolinformation FkSchoolinfo { get; set; } = null!;
    }
}
