using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("commendationtypes")]
    public partial class Commendationtype
    {
        public Commendationtype()
        {
            Studentcommendations = new HashSet<Studentcommendation>();
        }

        [Key]
        [Column("commendationtypeid")]
        public Guid Commendationtypeid { get; set; }
        [Column("typename")]
        [StringLength(255)]
        public string Typename { get; set; } = null!;

        [InverseProperty("Commendationtype")]
        public virtual ICollection<Studentcommendation> Studentcommendations { get; set; }
    }
}
