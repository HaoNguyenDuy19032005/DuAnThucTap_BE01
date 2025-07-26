using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("qnathreads")]
    public partial class Qnathread
    {
        public Qnathread()
        {
            Qnaparticipants = new HashSet<Qnaparticipant>();
            Userthreadreadstatuses = new HashSet<Userthreadreadstatus>();
        }

        [Key]
        [Column("threadid")]
        public int Threadid { get; set; }
        [Column("classid")]
        public int Classid { get; set; }
        [Column("creatorid")]
        public int Creatorid { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("replycount")]
        public int? Replycount { get; set; }
        [Column("lastactivityat")]
        public DateTime? Lastactivityat { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Classid")]
        [InverseProperty("Qnathreads")]
        public virtual Class Class { get; set; } = null!;
        [ForeignKey("Creatorid")]
        [InverseProperty("Qnathreads")]
        public virtual User Creator { get; set; } = null!;
        [InverseProperty("Thread")]
        public virtual ICollection<Qnaparticipant> Qnaparticipants { get; set; }
        [InverseProperty("Thread")]
        public virtual ICollection<Userthreadreadstatus> Userthreadreadstatuses { get; set; }
    }
}
