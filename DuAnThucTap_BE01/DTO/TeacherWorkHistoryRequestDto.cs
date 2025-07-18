using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class TeacherWorkHistoryRequestDto : IValidatableObject
    {
        // Workhistoryid không cần ở đây vì nó sẽ được sinh tự động khi tạo mới
        // và được truyền qua URL cho các hành động Update/Delete.

        [Required(ErrorMessage = "ID Giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        // Có thể null nếu không có đơn vị công tác cụ thể
        public int? Operationunitid { get; set; }

        // Có thể null nếu không có phòng ban cụ thể
        public int? Departmentid { get; set; }

        public bool? Iscurrentschool { get; set; }

        [Required(ErrorMessage = "Vị trí công tác không được để trống.")]
        [StringLength(150, ErrorMessage = "Vị trí công tác không được vượt quá 150 ký tự.")]
        public string Positionheld { get; set; } = string.Empty; // Khởi tạo để tránh null

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateOnly Startdate { get; set; }

        public DateOnly? Enddate { get; set; } // Có thể null nếu là công việc hiện tại

        // Custom validation cho ngày bắt đầu và ngày kết thúc
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Startdate > DateOnly.FromDateTime(DateTime.Today))
            {
                yield return new ValidationResult(
                    "Ngày bắt đầu không được là một ngày trong tương lai.",
                    new[] { nameof(Startdate) }
                );
            }

            if (Enddate.HasValue && Startdate >= Enddate.Value)
            {
                yield return new ValidationResult(
                    "Ngày kết thúc phải sau ngày bắt đầu.",
                    new[] { nameof(Enddate) }
                );
            }
        }
    }
}
