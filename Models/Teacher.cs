using DuAnThucTapNhom3.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Departments = new HashSet<Department>();
        }
        [Key]
        public int Teacherid { get; set; }
        public string Fullname { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Department> Departments { get; set; }

        [JsonIgnore]
        public virtual ICollection<SchoolYearlyStatus>? SchoolYearlyStatuses { get; set; }
    }
}
