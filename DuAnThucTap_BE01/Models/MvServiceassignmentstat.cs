using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Keyless]
    public partial class MvServiceassignmentstat
    {
        [Column("servicecode")]
        [StringLength(10)]
        public string? Servicecode { get; set; }
        [Column("servicename")]
        [StringLength(50)]
        public string? Servicename { get; set; }
        [Column("assignedthisyear")]
        public long? Assignedthisyear { get; set; }
        [Column("assignedthismonth")]
        public long? Assignedthismonth { get; set; }
        [Column("assignedthisweek")]
        public long? Assignedthisweek { get; set; }
    }
}
