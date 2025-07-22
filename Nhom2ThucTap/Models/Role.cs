using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
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
        public int Roleid { get; set; }
        [Column("rolename")]
        [StringLength(100)]
        public string Rolename { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }

        [JsonIgnore]
        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }

        [JsonIgnore]
        [ForeignKey("Roleid")]
        [InverseProperty("Roles")]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
