using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnThucTapNhom3.Models
{
    public class SchoolYearlyStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSchoolYearlyStatus { get; set; }

        public int? StudentId { get; set; }
        public int? SchoolYearId { get; set; }

        public string? Status { get; set; } = null!; // e.g., Active, Graduated, Dropped Out
        public string? Note { get; set; }

        public int? ClassId { get; set; } = null;
        public int? GradelevelId { get; set; }
        public int? SemesterId { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; } // ID of the user who created or updated this record
        // Navigation
        public virtual Student? Student { get; set; }
        public virtual Schoolyear? SchoolYear { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual User? User { get; set; }
        public virtual Class? Class { get; set; }
        public virtual Gradelevel? Gradelevel { get; set; }
        public virtual Semester? Semester { get; set; }
    }
}
