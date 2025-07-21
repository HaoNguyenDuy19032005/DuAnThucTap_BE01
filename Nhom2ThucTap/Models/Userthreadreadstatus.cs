using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("userthreadreadstatus")]
    public partial class Userthreadreadstatus
    {
        [Key]
        [Column("userid")]
        public int Userid { get; set; }
        [Key]
        [Column("threadid")]
        public int Threadid { get; set; }
        [Column("lastreadtimestamp", TypeName = "timestamp without time zone")]
        public DateTime? Lastreadtimestamp { get; set; }

        [ForeignKey("Threadid")]
        [InverseProperty("Userthreadreadstatuses")]
        public virtual Qnathread Thread { get; set; } = null!;
        [ForeignKey("Userid")]
        [InverseProperty("Userthreadreadstatuses")]
        public virtual User User { get; set; } = null!;
    }
}
