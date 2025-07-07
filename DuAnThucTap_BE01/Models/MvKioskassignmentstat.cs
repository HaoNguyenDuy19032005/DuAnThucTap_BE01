using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Keyless]
    public partial class MvKioskassignmentstat
    {
        [Column("devicecode")]
        [StringLength(10)]
        public string? Devicecode { get; set; }
        [Column("devicename")]
        [StringLength(50)]
        public string? Devicename { get; set; }
        [Column("totalassignments")]
        public long? Totalassignments { get; set; }
    }
}
