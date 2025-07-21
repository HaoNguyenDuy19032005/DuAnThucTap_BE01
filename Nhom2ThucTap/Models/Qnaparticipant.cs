using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("qnaparticipants")]
    public partial class Qnaparticipant
    {
        [Key]
        [Column("threadid")]
        public int Threadid { get; set; }
        [Key]
        [Column("userid")]
        public int Userid { get; set; }
        [Column("firstparticipatedat", TypeName = "timestamp without time zone")]
        public DateTime? Firstparticipatedat { get; set; }

        [ForeignKey("Threadid")]
        [InverseProperty("Qnaparticipants")]
        public virtual Qnathread Thread { get; set; } = null!;
        [ForeignKey("Userid")]
        [InverseProperty("Qnaparticipants")]
        public virtual User User { get; set; } = null!;
    }
}
