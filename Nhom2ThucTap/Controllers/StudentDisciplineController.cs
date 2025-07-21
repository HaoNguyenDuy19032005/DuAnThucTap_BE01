using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDisciplineController : ControllerBase
    {
        private readonly IStudentDisciplineService _service;
        private readonly AppDbContext _context;

        public StudentDisciplineController(IStudentDisciplineService service, AppDbContext context)
        {
            _service = service;
            _context = context;
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

            var (disciplines, totalCount) = await _service.GetPagedAsync(page, pageSize);

            return Ok(new
            {
                data = disciplines,
                pagination = new
                {
                    currentPage = page,
                    pageSize,
                    totalItems = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy bản ghi kỷ luật." });

            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] StudentDisciplineCreateDto dto)
        {
            var errors = ValidateDto(dto);

            // Bắt lỗi file ảnh
            var fileErrors = ValidateFile(dto.Attachmentfile);
            foreach (var err in fileErrors)
            {
                if (!errors.ContainsKey(err.Key))
                    errors[err.Key] = new List<string>();
                errors[err.Key].AddRange(err.Value);
            }

            if (errors.Any()) return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            var checkFk = await ValidateForeignKeys(dto);
            if (checkFk != null) return checkFk;

            string? filePath = await SaveFileAsync(dto.Attachmentfile);

            DateOnly? commendDate = ParseDate(dto.Commendationdate);

            var newDiscipline = new Studentdiscipline
            {
                Studentid = dto.Studentid!.Value,
                Semesterid = dto.Semesterid!.Value,
                Schoolinfoid = dto.Schoolinfoid!.Value,
                Disciplinetypeid = dto.Disciplinetypeid!.Value,
                Commendationdate = commendDate,
                Content = dto.Content,
                Attachmenturl = filePath,
                Createdat = DateTime.UtcNow
            };

            await _service.AddAsync(newDiscipline);
            return Ok(new { message = "Tạo bản ghi kỷ luật thành công." });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] StudentDisciplineCreateDto dto)
        {
            var errors = ValidateDto(dto);

            // Bắt lỗi file ảnh
            var fileErrors = ValidateFile(dto.Attachmentfile);
            foreach (var err in fileErrors)
            {
                if (!errors.ContainsKey(err.Key))
                    errors[err.Key] = new List<string>();
                errors[err.Key].AddRange(err.Value);
            }

            if (errors.Any()) return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật." });

            var checkFk = await ValidateForeignKeys(dto);
            if (checkFk != null) return checkFk;

            string? filePath = existing.Attachmenturl;
            if (dto.Attachmentfile != null && dto.Attachmentfile.Length > 0)
                filePath = await SaveFileAsync(dto.Attachmentfile);

            DateOnly? commendDate = ParseDate(dto.Commendationdate);

            existing.Studentid = dto.Studentid!.Value;
            existing.Semesterid = dto.Semesterid!.Value;
            existing.Schoolinfoid = dto.Schoolinfoid!.Value;
            existing.Disciplinetypeid = dto.Disciplinetypeid!.Value;
            existing.Commendationdate = commendDate;
            existing.Content = dto.Content;
            existing.Attachmenturl = filePath;

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

        // ======================= Hỗ trợ ======================

        private Dictionary<string, List<string>> ValidateDto(StudentDisciplineCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();

            void Add(string key, string msg)
            {
                if (!errors.ContainsKey(key)) errors[key] = new();
                errors[key].Add(msg);
            }

            if (dto.Studentid == null) Add("Studentid", "Mã sinh viên không được để trống.");
            if (dto.Semesterid == null) Add("Semesterid", "Học kỳ không được để trống.");
            if (dto.Schoolinfoid == null) Add("Schoolinfoid", "Trường không được để trống.");
            if (dto.Disciplinetypeid == null) Add("Disciplinetypeid", "Loại kỷ luật không được để trống.");

            if (!string.IsNullOrWhiteSpace(dto.Commendationdate) &&
                !DateOnly.TryParse(dto.Commendationdate, out _))
                Add("Commendationdate", "Ngày không hợp lệ. Định dạng: yyyy-MM-dd");

            return errors;
        }

        private Dictionary<string, List<string>> ValidateFile(IFormFile? file)
        {
            var errors = new Dictionary<string, List<string>>();
            if (file == null || file.Length == 0)
                return errors; // không có file thì không lỗi

            // Kiểm tra kích thước file (ví dụ max 5MB)
            const long maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                errors.TryAdd("Attachmentfile", new List<string>());
                errors["Attachmentfile"].Add("Kích thước file không được vượt quá 5MB.");
            }

            // Kiểm tra định dạng file
            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                errors.TryAdd("Attachmentfile", new List<string>());
                errors["Attachmentfile"].Add("Chỉ cho phép các định dạng ảnh: jpg, jpeg, png, gif.");
            }

            return errors;
        }

        private DateOnly? ParseDate(string? dateStr)
        {
            return DateOnly.TryParse(dateStr, out var date) ? date : null;
        }

        private async Task<string?> SaveFileAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            var uploadFolder = Path.Combine("wwwroot", "uploads");
            Directory.CreateDirectory(uploadFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(uploadFolder, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{fileName}";
        }

        private async Task<IActionResult?> ValidateForeignKeys(StudentDisciplineCreateDto dto)
        {
            if (!await _context.Students.AnyAsync(s => s.Studentid == dto.Studentid))
                return BadRequest(new { message = "Mã sinh viên không tồn tại." });

            if (!await _context.Semesters.AnyAsync(s => s.Semesterid == dto.Semesterid))
                return BadRequest(new { message = "Học kỳ không tồn tại." });

            if (!await _context.Schoolinformations.AnyAsync(s => s.Schoolinfoid == dto.Schoolinfoid))
                return BadRequest(new { message = "Trường không tồn tại." });

            if (!await _context.Disciplinetypes.AnyAsync(s => s.Disciplinetypeid == dto.Disciplinetypeid))
                return BadRequest(new { message = "Loại kỷ luật không tồn tại." });

            return null;
        }
    }
}
