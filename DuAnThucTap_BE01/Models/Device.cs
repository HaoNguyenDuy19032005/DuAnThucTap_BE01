using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("devices")]
    public partial class Device
    {
        public Device()
        {
            Assignments = new HashSet<Assignment>();
        }

        [Key]
        [Column("devicecode")]
        [StringLength(10)]
        public string Devicecode { get; set; } = null!;
        [Column("devicename")]
        [StringLength(50)]
        public string? Devicename { get; set; }
        [Column("ipaddress")]
        [StringLength(15)]
        public string? Ipaddress { get; set; }
        [Column("username")]
        [StringLength(50)]
        public string? Username { get; set; }
        [Column("password")]
        [StringLength(50)]
        public string? Password { get; set; }
        [Column("connected")]
        public bool? Connected { get; set; }
        [Column("operationstatus")]
        public bool? Operationstatus { get; set; }

        [InverseProperty("DevicecodeNavigation")]
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
