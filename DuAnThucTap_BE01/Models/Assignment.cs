using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("assignments")]
    public partial class Assignment
    {
        [Key]
        [Column("code")]
        public int Code { get; set; }
        [Column("customername")]
        [StringLength(50)]
        public string? Customername { get; set; }
        [Column("customeremail")]
        [StringLength(50)]
        public string? Customeremail { get; set; }
        [Column("telephone")]
        [StringLength(20)]
        public string? Telephone { get; set; }
        [Column("assignmentdate")]
        public DateTime? Assignmentdate { get; set; }
        [Column("expiredate")]
        public DateTime? Expiredate { get; set; }
        [Column("status")]
        public short? Status { get; set; }
        [Column("servicecode")]
        [StringLength(10)]
        public string? Servicecode { get; set; }
        [Column("devicecode")]
        [StringLength(10)]
        public string? Devicecode { get; set; }

        [ForeignKey("Devicecode")]
        [InverseProperty("Assignments")]
        public virtual Device? DevicecodeNavigation { get; set; }
        [ForeignKey("Servicecode")]
        [InverseProperty("Assignments")]
        public virtual Service? ServicecodeNavigation { get; set; }
    }
}
