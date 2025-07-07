using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("services")]
    public partial class Service
    {
        public Service()
        {
            Assignments = new HashSet<Assignment>();
        }

        [Key]
        [Column("servicecode")]
        [StringLength(10)]
        public string Servicecode { get; set; } = null!;
        [Column("servicename")]
        [StringLength(50)]
        public string? Servicename { get; set; }
        [Column("description")]
        [StringLength(100)]
        public string? Description { get; set; }
        [Column("isinoperation")]
        public bool? Isinoperation { get; set; }

        [InverseProperty("ServicecodeNavigation")]
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
