using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("roles")]
    [Index("Roleid", Name = "roles_roleid_key", IsUnique = true)]
    public partial class Role
    {
        public Role()
        {
            Rolepermissions = new HashSet<Rolepermission>();
            Users = new HashSet<User>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("roleid")]
        [StringLength(255)]
        public string Roleid { get; set; } = null!;
        [Column("rolename")]
        [StringLength(255)]
        public string Rolename { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;

        public virtual ICollection<Rolepermission> Rolepermissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
