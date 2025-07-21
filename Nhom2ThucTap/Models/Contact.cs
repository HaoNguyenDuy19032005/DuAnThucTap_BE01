using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("contacts")]
    public partial class Contact
    {
        [Key]
        [Column("contactid")]
        public int Contactid { get; set; }
        [Column("teacherid")]
        public int? Teacherid { get; set; }
        [Column("relationship")]
        [StringLength(100)]
        public string? Relationship { get; set; }
        [Column("fullname")]
        [StringLength(150)]
        public string? Fullname { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("phonenumber")]
        [StringLength(20)]
        public string? Phonenumber { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Contacts")]
        public virtual Teacher? Teacher { get; set; }
    }
}
