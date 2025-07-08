using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
	public partial class Semester
	{
		public int Semesterid { get; set; }
		public int Schoolyearid { get; set; }
		public string Semestername { get; set; } = null!;
		public DateTime? Startdate { get; set; }
		public DateTime? Enddate { get; set; }
		public DateTime? Createdat { get; set; }
		public DateTime? Updatedat { get; set; }
		[JsonIgnore]
		public virtual Schoolyear? Schoolyear { get; set; } = null!;
	}
}
