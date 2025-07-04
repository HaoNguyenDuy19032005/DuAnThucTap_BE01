using DuAnThucTap_BE01.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Department
    {
        public Department()
        {
            Subjects = new HashSet<Subject>();
        }

        public int Departmentid { get; set; }
        public string Departmentname { get; set; } = null!;
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public int? Headteacherid { get; set; }
        [JsonIgnore]

        public virtual Teacher? Headteacher { get; set; }
        [JsonIgnore]
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
