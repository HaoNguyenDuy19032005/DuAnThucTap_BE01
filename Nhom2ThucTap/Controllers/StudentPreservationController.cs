using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentPreservationController : ControllerBase
    {
        private readonly IStudentPreservationService _service;

        public StudentPreservationController(IStudentPreservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Giá trị page và pageSize phải lớn hơn 0." });

            var (data, totalCount) = await _service.GetPagedAsync(page, pageSize);

            var response = new
            {
                data = data,
                pagination = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalItems = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var preservation = await _service.GetByIdAsync(id);
            if (preservation == null)
                return NotFound(new { message = "Không tìm thấy bản ghi bảo lưu." });

            return Ok(preservation);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] StudentPreservationCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            // Kiểm tra các trường bắt buộc
            if (dto.Studentid == null)
                AddError("Studentid", "Mã sinh viên không được để trống.");
            if (dto.Classid == null)
                AddError("Classid", "Mã lớp không được để trống.");
            if (dto.Semesterid == null)
                AddError("Semesterid", "Mã học kỳ không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Preservationdate))
                AddError("Preservationdate", "Ngày bảo lưu không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Preservationduration))
                AddError("Preservationduration", "Thời gian bảo lưu không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Reason))
                AddError("Reason", "Lý do không được để trống.");

            // Parse ngày
            DateOnly? parsedDate = null;
            if (!string.IsNullOrWhiteSpace(dto.Preservationdate))
            {
                if (!DateOnly.TryParse(dto.Preservationdate, out var d))
                    AddError("Preservationdate", "Ngày không hợp lệ. Định dạng: yyyy-MM-dd");
                else
                    parsedDate = d;
            }

            // Kiểm tra khoá ngoại
            if (dto.Studentid != null && !await _service.ExistsStudentAsync(dto.Studentid.Value))
                AddError("Studentid", "Mã sinh viên không tồn tại.");

            if (dto.Classid != null && !await _service.ExistsClassAsync(dto.Classid.Value))
                AddError("Classid", "Mã lớp không tồn tại.");

            if (dto.Semesterid != null && !await _service.ExistsSemesterAsync(dto.Semesterid.Value))
                AddError("Semesterid", "Mã học kỳ không tồn tại.");

            // Bắt lỗi file ảnh
            var fileErrors = ValidateFile(dto.Attachmentfile);
            foreach (var err in fileErrors)
            {
                if (!errors.ContainsKey(err.Key))
                    errors[err.Key] = new List<string>();
                errors[err.Key].AddRange(err.Value);
            }

            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            // Lưu file nếu có
            string? filePath = null;
            if (dto.Attachmentfile != null && dto.Attachmentfile.Length > 0)
            {
                var folder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(folder);
                var filename = $"{Guid.NewGuid()}_{dto.Attachmentfile.FileName}";
                var fullPath = Path.Combine(folder, filename);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await dto.Attachmentfile.CopyToAsync(stream);
                filePath = $"/uploads/{filename}";
            }

            var entity = new Studentpreservation
            {
                Studentid = dto.Studentid!.Value,
                Classid = dto.Classid!.Value,
                Semesterid = dto.Semesterid!.Value,
                Preservationdate = parsedDate!.Value,
                Preservationduration = dto.Preservationduration!,
                Reason = dto.Reason!,
                Attachmenturl = filePath,
                Createdat = DateTime.Now
            };

            await _service.AddAsync(entity);
            return Ok(new { message = "Tạo bản ghi thành công." });
        }


        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] StudentPreservationCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            if (dto.Studentid == null)
                AddError("Studentid", "Mã sinh viên không được để trống.");
            if (dto.Classid == null)
                AddError("Classid", "Mã lớp không được để trống.");
            if (dto.Semesterid == null)
                AddError("Semesterid", "Mã học kỳ không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Preservationdate))
                AddError("Preservationdate", "Ngày bảo lưu không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Preservationduration))
                AddError("Preservationduration", "Thời gian bảo lưu không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Reason))
                AddError("Reason", "Lý do không được để trống.");

            DateOnly? parsedDate = null;
            if (!string.IsNullOrWhiteSpace(dto.Preservationdate))
            {
                if (!DateOnly.TryParse(dto.Preservationdate, out var d))
                    AddError("Preservationdate", "Ngày không hợp lệ. Định dạng: yyyy-MM-dd");
                else
                    parsedDate = d;
            }

            if (dto.Studentid != null && !await _service.ExistsStudentAsync(dto.Studentid.Value))
                AddError("Studentid", "Mã sinh viên không tồn tại.");
            if (dto.Classid != null && !await _service.ExistsClassAsync(dto.Classid.Value))
                AddError("Classid", "Mã lớp không tồn tại.");
            if (dto.Semesterid != null && !await _service.ExistsSemesterAsync(dto.Semesterid.Value))
                AddError("Semesterid", "Mã học kỳ không tồn tại.");

            // Bắt lỗi file ảnh
            var fileErrors = ValidateFile(dto.Attachmentfile);
            foreach (var err in fileErrors)
            {
                if (!errors.ContainsKey(err.Key))
                    errors[err.Key] = new List<string>();
                errors[err.Key].AddRange(err.Value);
            }

            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật." });

            string? filePath = existing.Attachmenturl;
            if (dto.Attachmentfile != null && dto.Attachmentfile.Length > 0)
            {
                var folder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(folder);
                var filename = $"{Guid.NewGuid()}_{dto.Attachmentfile.FileName}";
                var fullPath = Path.Combine(folder, filename);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await dto.Attachmentfile.CopyToAsync(stream);
                filePath = $"/uploads/{filename}";
            }

            existing.Studentid = dto.Studentid!.Value;
            existing.Classid = dto.Classid!.Value;
            existing.Semesterid = dto.Semesterid!.Value;
            existing.Preservationdate = parsedDate!.Value;
            existing.Preservationduration = dto.Preservationduration!;
            existing.Reason = dto.Reason!;
            existing.Attachmenturl = filePath;
            existing.Updatedat = DateTime.Now;

            await _service.UpdateAsync(id, existing);
            return Ok(new { message = "Cập nhật bản ghi thành công." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Không tìm thấy bản ghi để xóa." });

                return Ok(new { message = "Xóa bản ghi thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.Message });
            }
        }

        // Hàm kiểm tra file ảnh
        private Dictionary<string, List<string>> ValidateFile(Microsoft.AspNetCore.Http.IFormFile? file)
        {
            var errors = new Dictionary<string, List<string>>();
            if (file == null || file.Length == 0)
                return errors; // không có file => không lỗi

            // Giới hạn kích thước file 5MB
            const long maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                errors.TryAdd("Attachmentfile", new List<string>());
                errors["Attachmentfile"].Add("Kích thước file không được vượt quá 5MB.");
            }

            // Kiểm tra định dạng file
            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                errors.TryAdd("Attachmentfile", new List<string>());
                errors["Attachmentfile"].Add("Chỉ cho phép các định dạng ảnh: jpg, jpeg, png, gif.");
            }

            return errors;
        }
    }
}
