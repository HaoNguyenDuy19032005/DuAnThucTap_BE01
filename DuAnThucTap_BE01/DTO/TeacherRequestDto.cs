using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class TeacherRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Họ và tên phải có độ dài từ 2 đến 150 ký tự.")]
        public string Fullname { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateOnly? Dateofbirth { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống.")]
        [StringLength(10, ErrorMessage = "Giới tính không được vượt quá 10 ký tự.")]
        public string? Gender { get; set; }

        [StringLength(50, ErrorMessage = "Dân tộc không được vượt quá 50 ký tự.")]
        public string? Ethnicity { get; set; }

        public DateOnly? Hiredate { get; set; }

        [StringLength(100, ErrorMessage = "Quốc tịch không được vượt quá 100 ký tự.")]
        public string? Nationality { get; set; }

        [StringLength(50, ErrorMessage = "Tôn giáo không được vượt quá 50 ký tự.")]
        public string? Religion { get; set; }

        [Required(ErrorMessage = "Trạng thái công tác không được để trống.")]
        [StringLength(100, ErrorMessage = "Trạng thái công tác không được vượt quá 100 ký tự.")]
        public string? Status { get; set; }

        [StringLength(150, ErrorMessage = "Bí danh không được vượt quá 150 ký tự.")]
        public string? Alias { get; set; }

        [Required(ErrorMessage = "Tỉnh/Thành phố không được để trống.")]
        [StringLength(100, ErrorMessage = "Tỉnh/Thành phố không được vượt quá 100 ký tự.")]
        public string? AddressProvincecity { get; set; }

        [Required(ErrorMessage = "Phường/Xã không được để trống.")]
        [StringLength(100, ErrorMessage = "Phường/Xã không được vượt quá 100 ký tự.")]
        public string? AddressWard { get; set; }

        [Required(ErrorMessage = "Quận/Huyện không được để trống.")]
        [StringLength(100, ErrorMessage = "Quận/Huyện không được vượt quá 100 ký tự.")]
        public string? AddressDistrict { get; set; }

        [StringLength(255, ErrorMessage = "Địa chỉ số nhà, tên đường không được vượt quá 255 ký tự.")]
        public string? AddressStreet { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        [StringLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string? Phonenumber { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Dateofjoiningtheparty { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Dateofjoininggroup { get; set; }
        public bool? Ispartymember { get; set; }

        [Required(ErrorMessage = "Khoa/Bộ môn không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Khoa/Bộ môn không hợp lệ.")]
        public int? Departmentid { get; set; }

        [Required(ErrorMessage = "Môn học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Môn học không hợp lệ.")]
        public int? Subjectid { get; set; }

        [Required(ErrorMessage = "Năm học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Năm học không hợp lệ.")]
        public int? Schoolyearid { get; set; }

        // Chuyển logic validation tuổi từ Model sang DTO
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
                    yield break; // Dừng validation nếu ngày sinh không hợp lệ
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