using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;
using System.IO;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentcommendationController : ControllerBase
    {
        private readonly IStudentcommendationService _service;
        private readonly AppDbContext _context;

        public StudentcommendationController(IStudentcommendationService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commendations = await _service.GetAllAsync();
            return Ok(commendations);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Giá trị page và pageSize phải lớn hơn 0." });

            var (commendations, totalCount) = await _service.GetPagedAsync(page, pageSize);

            var result = new
            {
                data = commendations,
                pagination = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalItems = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var commendation = await _service.GetByIdAsync(id);
            if (commendation == null)
                return NotFound(new { message = "Không tìm thấy bản ghi khen thưởng." });

            return Ok(commendation);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateWithFile([FromForm] StudentCommendationCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            // Validate bắt buộc
            if (dto.Studentid == null)
                AddError("Studentid", "Không được để trống mã sinh viên.");
            if (dto.Semesterid == null)
                AddError("Semesterid", "Không được để trống học kỳ.");
            if (dto.Schoolinfoid == null)
                AddError("Schoolinfoid", "Không được để trống trường.");
            if (dto.Commendationtypeid == null)
                AddError("Commendationtypeid", "Không được để trống loại khen thưởng.");
            if (string.IsNullOrWhiteSpace(dto.Content))
                AddError("Content", "Không được để trống nội dung.");

            DateOnly parsedDate;
            if (string.IsNullOrWhiteSpace(dto.Commendationdate))
            {
                AddError("Commendationdate", "Không được để trống ngày khen thưởng.");
            }
            else if (!DateOnly.TryParse(dto.Commendationdate, out parsedDate))
            {
                AddError("Commendationdate", "Ngày khen thưởng không đúng định dạng (yyyy-MM-dd).");
            }

            // Validate foreign keys tồn tại trong DB
            if (dto.Studentid != null && !await _context.Students.AnyAsync(s => s.Studentid == dto.Studentid))
                AddError("Studentid", "Mã sinh viên không tồn tại.");

            if (dto.Semesterid != null && !await _context.Semesters.AnyAsync(s => s.Semesterid == dto.Semesterid))
                AddError("Semesterid", "Học kỳ không tồn tại.");

            if (dto.Schoolinfoid != null && !await _context.Schoolinformations.AnyAsync(s => s.Schoolinfoid == dto.Schoolinfoid))
                AddError("Schoolinfoid", "Trường không tồn tại.");

            if (dto.Commendationtypeid != null && !await _context.Commendationtypes.AnyAsync(s => s.Commendationtypeid == dto.Commendationtypeid))
                AddError("Commendationtypeid", "Loại khen thưởng không tồn tại.");

            // Validate file ảnh
            if (dto.Attachmentfile is { Length: > 0 })
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var extension = Path.GetExtension(dto.Attachmentfile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    AddError("Attachmentfile", "Chỉ được gửi file ảnh có định dạng jpg, jpeg, png, gif, bmp.");
                }
            }

            if (errors.Any())
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
            }

            // Lưu file nếu có
            string? filePath = null;
            if (dto.Attachmentfile is { Length: > 0 })
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.Attachmentfile.FileName}";
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.Attachmentfile.CopyToAsync(stream);
                }

                filePath = $"/uploads/{fileName}";
            }

            var commendation = new Studentcommendation
            {
                Studentid = dto.Studentid!.Value,
                Semesterid = dto.Semesterid!.Value,
                Schoolinfoid = dto.Schoolinfoid!.Value,
                Commendationtypeid = dto.Commendationtypeid!.Value,
                Commendationdate = parsedDate,
                Content = dto.Content,
                Attachmenturl = filePath,
                Createdat = DateTime.UtcNow
            };

            await _service.AddAsync(commendation);
            return Ok(new { message = "Tạo bản ghi khen thưởng thành công." });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateWithFile(int id, [FromForm] StudentCommendationCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            // Validate bắt buộc
            if (dto.Studentid == null)
                AddError("Studentid", "Không được để trống mã sinh viên.");
            if (dto.Semesterid == null)
                AddError("Semesterid", "Không được để trống học kỳ.");
            if (dto.Schoolinfoid == null)
                AddError("Schoolinfoid", "Không được để trống trường.");
            if (dto.Commendationtypeid == null)
                AddError("Commendationtypeid", "Không được để trống loại khen thưởng.");
            if (string.IsNullOrWhiteSpace(dto.Content))
                AddError("Content", "Không được để trống nội dung.");

            DateOnly parsedDate;
            if (string.IsNullOrWhiteSpace(dto.Commendationdate))
                AddError("Commendationdate", "Không được để trống ngày khen thưởng.");
            else if (!DateOnly.TryParse(dto.Commendationdate, out parsedDate))
                AddError("Commendationdate", "Ngày khen thưởng không đúng định dạng (yyyy-MM-dd).");

            // Validate foreign keys
            if (dto.Studentid != null && !await _context.Students.AnyAsync(s => s.Studentid == dto.Studentid))
                AddError("Studentid", "Mã sinh viên không tồn tại.");
            if (dto.Semesterid != null && !await _context.Semesters.AnyAsync(s => s.Semesterid == dto.Semesterid))
                AddError("Semesterid", "Học kỳ không tồn tại.");
            if (dto.Schoolinfoid != null && !await _context.Schoolinformations.AnyAsync(s => s.Schoolinfoid == dto.Schoolinfoid))
                AddError("Schoolinfoid", "Trường không tồn tại.");
            if (dto.Commendationtypeid != null && !await _context.Commendationtypes.AnyAsync(s => s.Commendationtypeid == dto.Commendationtypeid))
                AddError("Commendationtypeid", "Loại khen thưởng không tồn tại.");

            // Validate file ảnh
            if (dto.Attachmentfile is { Length: > 0 })
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var extension = Path.GetExtension(dto.Attachmentfile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    AddError("Attachmentfile", "Chỉ được gửi file ảnh có định dạng jpg, jpeg, png, gif, bmp.");
                }
            }

            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật." });

            string? filePath = existing.Attachmenturl;

            // Lưu file mới nếu có
            if (dto.Attachmentfile is { Length: > 0 })
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.Attachmentfile.FileName}";
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.Attachmentfile.CopyToAsync(stream);
                }

                filePath = $"/uploads/{fileName}";
            }

            // Cập nhật dữ liệu
            existing.Studentid = dto.Studentid!.Value;
            existing.Semesterid = dto.Semesterid!.Value;
            existing.Schoolinfoid = dto.Schoolinfoid!.Value;
            existing.Commendationtypeid = dto.Commendationtypeid!.Value;
            existing.Commendationdate = parsedDate;
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
    }
}
