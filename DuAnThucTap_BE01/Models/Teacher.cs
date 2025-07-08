using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Models
{
	public class Teacher
	{
		[Key]
		public int TeacherID { get; set; }

		public string TeacherCode { get; set; }
		public string FullName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Gender { get; set; }
		public DateTime HireDate { get; set; }
		public string Ethnicity { get; set; }
		public string Nationality { get; set; }
		public string Religion { get; set; }
		public string Status { get; set; }
		public string Alias { get; set; }

		public string Address_ProvinceCity { get; set; }
		public string Address_Ward { get; set; }
		public string Address_District { get; set; }
		public string Address_Street { get; set; }

		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public bool IsUnionMember { get; set; }
		public DateTime? UnionAdmissionDate { get; set; }
		public bool IsPartyMember { get; set; }
		public DateTime? PartyAdmissionDate { get; set; }

		public int DepartmentId { get; set; }
		public int SchoolYearID { get; set; }
		public int ContactID { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation Properties
		public virtual ICollection<ExamGrader> ExamGraders { get; set; }
		public virtual ICollection<Class> Classes { get; set; }
	}
}