using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("userthreadreadstatus")]
    public partial class Userthreadreadstatus
    {
        [Key]
        [Column("userid")]
        public Guid Userid { get; set; }
        [Key]
        [Column("threadid")]
        public Guid Threadid { get; set; }
        [Column("lastreadtimestamp")]
        public DateTime Lastreadtimestamp { get; set; }

        [ForeignKey("Threadid")]
        [InverseProperty("Userthreadreadstatuses")]
        public virtual Qnathread Thread { get; set; } = null!;
        [ForeignKey("Userid")]
        [InverseProperty("Userthreadreadstatuses")]
        public virtual User User { get; set; } = null!;
    }
}
