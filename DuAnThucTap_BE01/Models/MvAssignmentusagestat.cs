using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Keyless]
    public partial class MvAssignmentusagestat
    {
        [Column("usedassignments")]
        public long? Usedassignments { get; set; }
        [Column("unusedassignments")]
        public long? Unusedassignments { get; set; }
    }
}
