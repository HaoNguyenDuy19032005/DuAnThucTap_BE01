using DuAnThucTapNhom2.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("students")]
    public partial class Student
    {
        public Student()
        {
            Classtransferhistories = new HashSet<Classtransferhistory>();
            Enrollments = new HashSet<Enrollment>();
            Grades = new HashSet<Grade>();
            Schooltransferhistories = new HashSet<Schooltransferhistory>();
            Studentcommendations = new HashSet<Studentcommendation>();
            Studentdisciplines = new HashSet<Studentdiscipline>();
            Studentexemptions = new HashSet<Studentexemption>();
            Studentpreservations = new HashSet<Studentpreservation>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            Studentyearlystatuses = new HashSet<Studentyearlystatus>();
        }

        [Key]
        [Column("studentid")]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống.")]
        [StringLength(100)]
        [Column("fullname")]
        public string Fullname { get; set; } = null!;

        [Required(ErrorMessage = "Giới tính không được để trống.")]
        [StringLength(10)]
        [Column("gender")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        [Column("dateofbirth", TypeName = "date")]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly Dateofbirth { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
        [StringLength(50)]
        [Column("studentcode")]
        public string Studentcode { get; set; } = null!;

        [Required(ErrorMessage = "Nơi sinh không được để trống.")]
        [Column("birthplace")]
        public string Birthplace { get; set; } = null!;

        [Required(ErrorMessage = "Ngày nhập học không được để trống.")]
        [Column("enrollmentdate", TypeName = "date")]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly Enrollmentdate { get; set; }

        [Required(ErrorMessage = "Dân tộc không được để trống.")]
        [StringLength(50)]
        [Column("ethnicity")]
        public string Ethnicity { get; set; } = null!;

        [Required(ErrorMessage = "Hình thức tuyển sinh không được để trống.")]
        [StringLength(50)]
        [Column("admissiontype")]
        public string Admissiontype { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Required(ErrorMessage = "Email không được để trống.")]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(20)]
        [Column("phonenumber")]
        public string Phonenumber { get; set; } = null!;

        [StringLength(50)]
        [Column("religion")]
        public string? Religion { get; set; }

        [StringLength(50)]
        [Column("status")]
        public string? Status { get; set; }

        [Column("address_provincecity")]
        public string? AddressProvincecity { get; set; }

        [Column("address_district")]
        public string? AddressDistrict { get; set; }

        [Column("address_ward")]
        public string? AddressWard { get; set; }

        [Column("address_street")]
        public string? AddressStreet { get; set; }

        [Required(ErrorMessage = "Họ tên cha không được để trống.")]
        [Column("fathername")]
        public string Fathername { get; set; } = null!;

        [Required(ErrorMessage = "Năm sinh cha không được để trống.")]
        [Range(1900, 2100, ErrorMessage = "Năm sinh cha không hợp lệ.")]
        [Column("fatherbirthyear")]
        public int Fatherbirthyear { get; set; }

        [Required(ErrorMessage = "Nghề nghiệp cha không được để trống.")]
        [Column("fatheroccupation")]
        public string Fatheroccupation { get; set; } = null!;

        [Required(ErrorMessage = "SĐT cha không được để trống.")]
        [Phone(ErrorMessage = "SĐT cha không hợp lệ.")]
        [StringLength(20)]
        [Column("phonenumberfather")]
        public string Phonenumberfather { get; set; } = null!;

        [Required(ErrorMessage = "Họ tên mẹ không được để trống.")]
        [Column("mothername")]
        public string Mothername { get; set; } = null!;

        [Required(ErrorMessage = "Năm sinh mẹ không được để trống.")]
        [Range(1900, 2100, ErrorMessage = "Năm sinh mẹ không hợp lệ.")]
        [Column("motherbirthyear")]
        public int Motherbirthyear { get; set; }

        [Required(ErrorMessage = "Nghề nghiệp mẹ không được để trống.")]
        [Column("motheroccupation")]
        public string Motheroccupation { get; set; } = null!;

        [Required(ErrorMessage = "Họ tên người giám hộ không được để trống.")]
        [Column("guardianname")]
        public string Guardianname { get; set; } = null!;

        [Required(ErrorMessage = "Năm sinh người giám hộ không được để trống.")]
        [Range(1900, 2100, ErrorMessage = "Năm sinh người giám hộ không hợp lệ.")]
        [Column("guardianbirthyear")]
        public int Guardianbirthyear { get; set; }

        [Required(ErrorMessage = "Nghề nghiệp người giám hộ không được để trống.")]
        [Column("guardianoccupation")]
        public string Guardianoccupation { get; set; } = null!;

        [Required(ErrorMessage = "SĐT người giám hộ không được để trống.")]
        [Phone(ErrorMessage = "SĐT người giám hộ không hợp lệ.")]
        [StringLength(20)]
        [Column("phonenumberguardian")]
        public string Phonenumberguardian { get; set; } = null!;

        [Column("profileimageurl")]
        public string? Profileimageurl { get; set; }

        [Column("createdat", TypeName = "timestamp with time zone")]
        public DateTime? Createdat { get; set; }

        [Column("updatedat", TypeName = "timestamp with time zone")]
        public DateTime? Updatedat { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual User? User { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Classtransferhistory> Classtransferhistories { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Grade> Grades { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Schooltransferhistory> Schooltransferhistories { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Studentcommendation> Studentcommendations { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Studentdiscipline> Studentdisciplines { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Studentexemption> Studentexemptions { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Studentpreservation> Studentpreservations { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<Studentyearlystatus> Studentyearlystatuses { get; set; }

        [JsonIgnore]
        [InverseProperty("Student")]
        public virtual ICollection<TestStudentSubmission> StudentSubmissions { get; set; } = new HashSet<TestStudentSubmission>();

    }
}
