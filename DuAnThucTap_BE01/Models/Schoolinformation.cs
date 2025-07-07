using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("schoolinformation")]
    [Index("Email", Name = "schoolinformation_email_key", IsUnique = true)]
    [Index("Standardcode", Name = "schoolinformation_standardcode_key", IsUnique = true)]
    public partial class Schoolinformation
    {
        public Schoolinformation()
        {
            Campuses = new HashSet<Campus>();
            Grades = new HashSet<Grade>();
            SchooltransferhistoryFromschools = new HashSet<Schooltransferhistory>();
            SchooltransferhistoryToschools = new HashSet<Schooltransferhistory>();
            Schoolyears = new HashSet<Schoolyear>();
            Studentcommendations = new HashSet<Studentcommendation>();
            Studentdisciplines = new HashSet<Studentdiscipline>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
        }

        [Key]
        [Column("schoolinfoid")]
        public Guid Schoolinfoid { get; set; }
        [Column("schoolname")]
        [StringLength(255)]
        public string Schoolname { get; set; } = null!;
        [Column("standardcode")]
        [StringLength(50)]
        public string? Standardcode { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("province")]
        [StringLength(100)]
        public string? Province { get; set; }
        [Column("ward")]
        [StringLength(100)]
        public string? Ward { get; set; }
        [Column("district")]
        [StringLength(100)]
        public string? District { get; set; }
        [Column("schooltype")]
        [StringLength(100)]
        public string? Schooltype { get; set; }
        [Column("phonenumber")]
        [StringLength(20)]
        public string? Phonenumber { get; set; }
        [Column("faxnumber")]
        [StringLength(20)]
        public string? Faxnumber { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string? Email { get; set; }
        [Column("establishmentdate")]
        public DateOnly? Establishmentdate { get; set; }
        [Column("trainingmodel")]
        [StringLength(255)]
        public string? Trainingmodel { get; set; }
        [Column("websiteurl")]
        public string? Websiteurl { get; set; }
        [Column("principalname")]
        [StringLength(150)]
        public string? Principalname { get; set; }
        [Column("principalphone")]
        [StringLength(20)]
        public string? Principalphone { get; set; }
        [Column("logourl")]
        public string? Logourl { get; set; }

        [InverseProperty("Schoolinfo")]
        public virtual ICollection<Campus> Campuses { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Grade> Grades { get; set; }
        [InverseProperty("Fromschool")]
        public virtual ICollection<Schooltransferhistory> SchooltransferhistoryFromschools { get; set; }
        [InverseProperty("Toschool")]
        public virtual ICollection<Schooltransferhistory> SchooltransferhistoryToschools { get; set; }
        [InverseProperty("Schoolinfo")]
        public virtual ICollection<Schoolyear> Schoolyears { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Studentcommendation> Studentcommendations { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Studentdiscipline> Studentdisciplines { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
    }
}
