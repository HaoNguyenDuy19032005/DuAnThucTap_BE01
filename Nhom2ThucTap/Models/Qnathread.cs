using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
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
        public string? Title { get; set; }
        [Column("replycount")]
        public int? Replycount { get; set; }
        [Column("lastactivityat", TypeName = "timestamp without time zone")]
        public DateTime? Lastactivityat { get; set; }
        [Column("status")]
        public string? Status { get; set; }
        [Column("rowcreatedat", TypeName = "timestamp without time zone")]
        public DateTime? Rowcreatedat { get; set; }

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
