using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("contacts")]
    [Index("Contactid", Name = "contacts_contactid_key", IsUnique = true)]
    public partial class Contact
    {
        public Contact()
        {
            Teachers = new HashSet<Teacher>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("contactid")]
        [StringLength(255)]
        public string Contactid { get; set; } = null!;
        [Column("fullname")]
        [StringLength(255)]
        public string Fullname { get; set; } = null!;
        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; } = null!;
        [Column("phonenumber")]
        [StringLength(255)]
        public string Phonenumber { get; set; } = null!;

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
