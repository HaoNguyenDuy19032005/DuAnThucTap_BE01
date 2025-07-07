using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("roles")]
    [Index("Rolename", Name = "roles_rolename_key", IsUnique = true)]
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
            Permissions = new HashSet<Permission>();
        }

        [Key]
        [Column("roleid")]
        public Guid Roleid { get; set; }
        [Column("rolename")]
        [StringLength(100)]
        public string Rolename { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }

        [ForeignKey("Roleid")]
        [InverseProperty("Roles")]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
