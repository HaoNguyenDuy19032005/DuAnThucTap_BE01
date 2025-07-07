using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("qnaparticipants")]
    public partial class Qnaparticipant
    {
        [Key]
        [Column("threadid")]
        public Guid Threadid { get; set; }
        [Key]
        [Column("userid")]
        public Guid Userid { get; set; }
        [Column("firstparticipatedat")]
        public DateTime? Firstparticipatedat { get; set; }

        [ForeignKey("Threadid")]
        [InverseProperty("Qnaparticipants")]
        public virtual Qnathread Thread { get; set; } = null!;
        [ForeignKey("Userid")]
        [InverseProperty("Qnaparticipants")]
        public virtual User User { get; set; } = null!;
    }
}
