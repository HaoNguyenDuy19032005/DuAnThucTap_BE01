using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("users")]
    [Index("Email", Name = "users_email_key", IsUnique = true)]
    [Index("Studentid", Name = "users_studentid_key", IsUnique = true)]
    [Index("Teacherid", Name = "users_teacherid_key", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Announcements = new HashSet<Announcement>();
            Qnaparticipants = new HashSet<Qnaparticipant>();
            Qnathreads = new HashSet<Qnathread>();
            Usernotifications = new HashSet<Usernotification>();
            Userthreadreadstatuses = new HashSet<Userthreadreadstatus>();
        }

        [Key]
        [Column("userid")]
        public int Userid { get; set; }
        [Column("fullname")]
        [StringLength(150)]
        public string? Fullname { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("passwordhash")]
        public string Passwordhash { get; set; } = null!;
        [Column("roleid")]
        public int Roleid { get; set; }
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }
        [Column("teacherid")]
        public int? Teacherid { get; set; }
        [Column("studentid")]
        public int? Studentid { get; set; }

        [JsonIgnore]
        [ForeignKey("Roleid")]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("Studentid")]
        [InverseProperty("User")]
        public virtual Student? Student { get; set; }
        [JsonIgnore]
        [ForeignKey("Teacherid")]
        [InverseProperty("User")]
        public virtual Teacher? Teacher { get; set; }
        [JsonIgnore]
        [InverseProperty("Creator")]
        public virtual ICollection<Announcement> Announcements { get; set; }
        [JsonIgnore]
        [InverseProperty("User")]
        public virtual ICollection<Qnaparticipant> Qnaparticipants { get; set; }
        [JsonIgnore]
        [InverseProperty("Creator")]
        public virtual ICollection<Qnathread> Qnathreads { get; set; }
        [JsonIgnore]
        [InverseProperty("User")]
        public virtual ICollection<Usernotification> Usernotifications { get; set; }
        [JsonIgnore]
        [InverseProperty("User")]
        public virtual ICollection<Userthreadreadstatus> Userthreadreadstatuses { get; set; }
    }
}
