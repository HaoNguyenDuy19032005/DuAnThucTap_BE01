using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("campuses")]
    public partial class Campus
    {
        [Key]
        [Column("campusid")]
        public int Campusid { get; set; }
        [Column("schoolinfoid")]
        public int Schoolinfoid { get; set; }
        [Column("campusname")]
        [StringLength(255)]
        public string Campusname { get; set; } = null!;
        [Column("address")]
        public string? Address { get; set; }
        [Column("phonenumber")]
        [StringLength(20)]
        public string? Phonenumber { get; set; }
        [Column("imageurl")]
        public string? Imageurl { get; set; }
        [Column("contactpersonname")]
        [StringLength(150)]
        public string? Contactpersonname { get; set; }
        [Column("contactpersonmobile")]
        [StringLength(20)]
        public string? Contactpersonmobile { get; set; }
        [Column("contactpersonemail")]
        [StringLength(255)]
        public string? Contactpersonemail { get; set; }

        [ForeignKey("Schoolinfoid")]
        [InverseProperty("Campuses")]
        public virtual Schoolinformation Schoolinfo { get; set; } = null!;
    }
}
