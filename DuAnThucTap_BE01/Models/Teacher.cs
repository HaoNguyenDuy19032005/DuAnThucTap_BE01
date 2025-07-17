using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("teachers")]
    [Index("Email", Name = "teachers_email_key", IsUnique = true)]
    [Index("Teachercode", Name = "teachers_teachercode_key", IsUnique = true)]
    public partial class Teacher : IValidatableObject
    {
        public Teacher()
        {
            Blockleaders = new HashSet<Blockleader>();
            Classes = new HashSet<Class>();
            Contacts = new HashSet<Contact>();
            Courses = new HashSet<Course>();
            Departmentleaders = new HashSet<Departmentleader>();
            Examgraders = new HashSet<Examgrader>();
            Gradelevels = new HashSet<Gradelevel>();
            Lessons = new HashSet<Lesson>();
            Teacherconcurrentsubjects = new HashSet<Teacherconcurrentsubject>();
            Teachertraininghistories = new HashSet<Teachertraininghistory>();
            Teacherworkhistories = new HashSet<Teacherworkhistory>();
            Teacherworkstatushistories = new HashSet<Teacherworkstatushistory>();
            Teachingassignments = new HashSet<Teachingassignment>();
            Tests = new HashSet<Test>();
        }

        [Key]
        [Column("teacherid")]
        public int Teacherid { get; set; }

        [Column("teachercode")]
        [StringLength(50)]
        public string? Teachercode { get; set; } // Server tự sinh, không cần validation

        [Column("fullname")]
        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Họ và tên phải có độ dài từ 2 đến 150 ký tự.")]
        public string Fullname { get; set; } = null!;

        [Column("dateofbirth")]
        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        public DateOnly? Dateofbirth { get; set; }

        [Column("gender")]
        [Required(ErrorMessage = "Giới tính không được để trống.")]
        [StringLength(10)]
        public string? Gender { get; set; }

        [Column("ethnicity")]
        [StringLength(50)]
        public string? Ethnicity { get; set; }

        [Column("hiredate")]
        public DateOnly? Hiredate { get; set; }

        [Column("nationality")]
        [StringLength(100)]
        public string? Nationality { get; set; }

        [Column("religion")]
        [StringLength(50)]
        public string? Religion { get; set; }

        [Column("status")]
        [Required(ErrorMessage = "Trạng thái công tác không được để trống.")]
        [StringLength(100)]
        public string? Status { get; set; }

        [Column("alias")]
        [StringLength(150)]
        public string? Alias { get; set; }

        [Column("address_provincecity")]
        [Required(ErrorMessage = "Tỉnh/Thành phố không được để trống.")]
        [StringLength(100)]
        public string? AddressProvincecity { get; set; }

        [Column("address_ward")]
        [Required(ErrorMessage = "Phường/Xã không được để trống.")]
        [StringLength(100)]
        public string? AddressWard { get; set; }

        [Column("address_district")]
        [Required(ErrorMessage = "Quận/Huyện không được để trống.")]
        [StringLength(100)]
        public string? AddressDistrict { get; set; }

        [Column("address_street")]
        [StringLength(255)]
        public string? AddressStreet { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        [StringLength(255)]
        public string Email { get; set; } = null!;

        [Column("phonenumber")]
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(20)]
        public string? Phonenumber { get; set; }

        [Column("dateofjoiningtheparty")]
        public DateOnly? Dateofjoiningtheparty { get; set; }

        [Column("avatarurl")]
        [Url(ErrorMessage = "Đường dẫn ảnh đại diện (URL) không hợp lệ.")]
        [StringLength(255)]
        public string? Avatarurl { get; set; }

        [Column("dateofjoininggroup")]
        public DateOnly? Dateofjoininggroup { get; set; }

        [Column("ispartymember")]
        public bool? Ispartymember { get; set; }

        [Column("departmentid")]
        [Required(ErrorMessage = "Khoa/Bộ môn không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Khoa/Bộ môn không hợp lệ.")]
        public int? Departmentid { get; set; }

        [Column("subjectid")]
        [Required(ErrorMessage = "Môn học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Môn học không hợp lệ.")]
        public int? Subjectid { get; set; }

        [Column("schoolyearid")]
        [Required(ErrorMessage = "Năm học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Năm học không hợp lệ.")]
        public int? Schoolyearid { get; set; }

        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        // --- Navigation Properties ---
        [ForeignKey("Departmentid")]
        [InverseProperty("Teachers")]
        [JsonIgnore]
        public virtual Department? Department { get; set; }

        [ForeignKey("Schoolyearid")]
        [InverseProperty("Teachers")]
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }

        [ForeignKey("Subjectid")]
        [InverseProperty("Teachers")]
        [JsonIgnore]
        public virtual Subject? Subject { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual User? User { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Blockleader> Blockleaders { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Class> Classes { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Departmentleader> Departmentleaders { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Examgrader> Examgraders { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Gradelevel> Gradelevels { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Lesson> Lessons { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Teachertraininghistory> Teachertraininghistories { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Teacherworkhistory> Teacherworkhistories { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Teacherworkstatushistory> Teacherworkstatushistories { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Teachingassignment> Teachingassignments { get; set; }

        [InverseProperty("Teacher")]
        [JsonIgnore]
        public virtual ICollection<Test> Tests { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Dateofbirth.HasValue)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);

                if (Dateofbirth.Value > today)
                {
                    yield return new ValidationResult(
                        "Ngày sinh không thể ở tương lai.",
                        new[] { nameof(Dateofbirth) }
                    );
                    yield break;
                }

                var age = today.Year - Dateofbirth.Value.Year;
                if (Dateofbirth.Value > today.AddYears(-age))
                {
                    age--;
                }

                if (age < 22)
                {
                    yield return new ValidationResult(
                        "Giáo viên phải từ 22 tuổi trở lên.",
                        new[] { nameof(Dateofbirth) }
                    );
                }
                else if (age > 70)
                {
                    yield return new ValidationResult(
                        "Tuổi giáo viên không được quá 70 tuổi.",
                        new[] { nameof(Dateofbirth) }
                    );
                }
            }
        }

    }
}