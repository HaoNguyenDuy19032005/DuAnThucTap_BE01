using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;
using System.Text.RegularExpressions;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _env;

        public StudentsController(IStudentService studentService, IWebHostEnvironment env)
        {
            _studentService = studentService;
            _env = env;
        }

        private bool CheckModelState(out IActionResult? errorResult)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                errorResult = BadRequest(new { message = "Dữ liệu không hợp lệ.", errors });
                return false;
            }
            errorResult = null;
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (students, _) = await _studentService.GetPagedStudentsAsync(1, int.MaxValue, null);
            return Ok(new { message = "Lấy danh sách sinh viên thành công.", total = students.Count, data = students });
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? keyword = null)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Trang và kích thước trang phải lớn hơn 0." });

            var (students, total) = await _studentService.GetPagedStudentsAsync(page, pageSize, keyword);
            return Ok(new { message = "Lấy danh sách sinh viên thành công.", total, page, pageSize, data = students });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return student == null
                ? NotFound(new { message = "Không tìm thấy sinh viên với mã đã cung cấp." })
                : Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StudentCreateDto dto)
        {
            if (!CheckModelState(out var modelError))
                return modelError!;

            var errors = ValidateDto(dto, out var dob, out var enrollment);
            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors });

            string? imagePath = null;
            if (dto.ProfileImage is { Length: > 0 })
            {
                var (success, msg, path) = await SaveImageAsync(dto.ProfileImage);
                if (!success) return BadRequest(new { message = msg });
                imagePath = path;
            }

            var student = new Student
            {
                Fullname = dto.Fullname,
                Gender = dto.Gender,
                Dateofbirth = dob,
                Enrollmentdate = enrollment,
                Studentcode = dto.Studentcode,
                Birthplace = dto.Birthplace,
                Ethnicity = dto.Ethnicity,
                Admissiontype = dto.Admissiontype,
                Email = dto.Email,
                Phonenumber = dto.Phonenumber,
                Fathername = dto.Fathername,
                Fatherbirthyear = dto.Fatherbirthyear.Value,
                Fatheroccupation = dto.Fatheroccupation,
                Phonenumberfather = dto.Phonenumberfather,
                Mothername = dto.Mothername,
                Motherbirthyear = dto.Motherbirthyear.Value,
                Motheroccupation = dto.Motheroccupation,
                Guardianname = dto.Guardianname,
                Guardianbirthyear = dto.Guardianbirthyear.Value,
                Guardianoccupation = dto.Guardianoccupation,
                Phonenumberguardian = dto.Phonenumberguardian,
                Profileimageurl = imagePath,
                Createdat = DateTime.UtcNow
            };

            await _studentService.AddStudentAsync(student);
            return Ok(new { message = "Tạo sinh viên mới thành công.", data = student });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] StudentCreateDto dto)
        {
            if (!CheckModelState(out var modelError))
                return modelError!;

            var errors = ValidateDto(dto, out var dob, out var enrollment);
            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors });

            var existing = await _studentService.GetStudentByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Không tìm thấy sinh viên để cập nhật." });

            if (dto.ProfileImage is { Length: > 0 })
            {
                var (success, msg, path) = await SaveImageAsync(dto.ProfileImage);
                if (!success) return BadRequest(new { message = msg });
                existing.Profileimageurl = path;
            }

            existing.Fullname = dto.Fullname;
            existing.Gender = dto.Gender;
            existing.Dateofbirth = dob;
            existing.Enrollmentdate = enrollment;
            existing.Studentcode = dto.Studentcode;
            existing.Birthplace = dto.Birthplace;
            existing.Ethnicity = dto.Ethnicity;
            existing.Admissiontype = dto.Admissiontype;
            existing.Email = dto.Email;
            existing.Phonenumber = dto.Phonenumber;
            existing.Fathername = dto.Fathername;
            existing.Fatherbirthyear = dto.Fatherbirthyear.Value;
            existing.Fatheroccupation = dto.Fatheroccupation;
            existing.Phonenumberfather = dto.Phonenumberfather;
            existing.Mothername = dto.Mothername;
            existing.Motherbirthyear = dto.Motherbirthyear.Value;
            existing.Motheroccupation = dto.Motheroccupation;
            existing.Guardianname = dto.Guardianname;
            existing.Guardianbirthyear = dto.Guardianbirthyear.Value;
            existing.Guardianoccupation = dto.Guardianoccupation;
            existing.Phonenumberguardian = dto.Phonenumberguardian;
            existing.Updatedat = DateTime.UtcNow;

            await _studentService.UpdateStudentAsync(existing);
            return Ok(new { message = "Cập nhật thông tin sinh viên thành công.", data = existing });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok(new { message = "Xoá sinh viên thành công." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên với mã {id}.", chiTiet = ex.Message });
            }
        }

        private List<string> ValidateDto(StudentCreateDto dto, out DateOnly dob, out DateOnly enrollment)
        {
            var errors = new List<string>();
            dob = default;
            enrollment = default;

            // Bắt buộc
            if (string.IsNullOrWhiteSpace(dto.Fullname)) errors.Add("Họ và tên không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Gender)) errors.Add("Giới tính không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Studentcode)) errors.Add("Mã sinh viên không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Email)) errors.Add("Email không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Birthplace)) errors.Add("Nơi sinh không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Ethnicity)) errors.Add("Dân tộc không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Admissiontype)) errors.Add("Hình thức tuyển sinh không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Phonenumber)) errors.Add("Số điện thoại sinh viên không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Fathername)) errors.Add("Họ tên cha không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Fatheroccupation)) errors.Add("Nghề nghiệp cha không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Mothername)) errors.Add("Họ tên mẹ không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Motheroccupation)) errors.Add("Nghề nghiệp mẹ không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Guardianname)) errors.Add("Tên người giám hộ không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Guardianoccupation)) errors.Add("Nghề nghiệp người giám hộ không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Phonenumberfather)) errors.Add("Số điện thoại cha không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Phonenumberguardian)) errors.Add("Số điện thoại người giám hộ không được để trống.");

            // Ngày tháng
            if (!DateOnly.TryParse(dto.Dateofbirth, out dob))
                errors.Add("Ngày sinh không hợp lệ. Định dạng đúng là yyyy-MM-dd.");
            if (!DateOnly.TryParse(dto.Enrollmentdate, out enrollment))
                errors.Add("Ngày nhập học không hợp lệ. Định dạng đúng là yyyy-MM-dd.");

            // Email
            if (!string.IsNullOrWhiteSpace(dto.Email) && !Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Email không hợp lệ. Vui lòng nhập đúng định dạng.");

            // Số điện thoại
            var phoneFields = new (string value, string errorMsg)[]
            {
                (dto.Phonenumber, "Số điện thoại sinh viên không hợp lệ. Phải bắt đầu bằng số 0 và có 10 hoặc 11 chữ số."),
                (dto.Phonenumberfather, "Số điện thoại của cha không hợp lệ. Phải bắt đầu bằng số 0 và có 10 hoặc 11 chữ số."),
                (dto.Phonenumberguardian, "Số điện thoại của người giám hộ không hợp lệ. Phải bắt đầu bằng số 0 và có 10 hoặc 11 chữ số.")
            };

            foreach (var (value, msg) in phoneFields)
            {
                if (!string.IsNullOrWhiteSpace(value) && !Regex.IsMatch(value, @"^0\d{9,10}$"))
                    errors.Add(msg);
            }

            return errors;
        }

        private async Task<(bool success, string message, string path)> SaveImageAsync(IFormFile file)
        {
            var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExts.Contains(ext))
                return (false, "Chỉ chấp nhận các định dạng ảnh: jpg, jpeg, png, gif.", "");

            var folder = Path.Combine(_env.WebRootPath, "uploads", "students");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return (true, "", $"/uploads/students/{fileName}");
        }
    }
}
