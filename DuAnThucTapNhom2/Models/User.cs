using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("users")]
    [Index("Userid", Name = "users_userid_key", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Files = new HashSet<File>();
            Notifications = new HashSet<Notification>();
            Systemsettings = new HashSet<Systemsetting>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("userid")]
        [StringLength(255)]
        public string Userid { get; set; } = null!;
        [Column("fullname")]
        [StringLength(255)]
        public string Fullname { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("passwordhash")]
        [StringLength(255)]
        public string Passwordhash { get; set; } = null!;
        [Column("roleid")]
        [StringLength(255)]
        public string Roleid { get; set; } = null!;
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Systemsetting> Systemsettings { get; set; }
    }
}
