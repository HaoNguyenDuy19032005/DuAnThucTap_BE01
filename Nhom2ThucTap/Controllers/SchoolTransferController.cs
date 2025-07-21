using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;
using System.IO;
using System.Linq;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolTransferHistoryController : ControllerBase
    {
        private readonly ISchoolTransferHistoryService _service;

        public SchoolTransferHistoryController(ISchoolTransferHistoryService service)
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

            var (transfers, totalCount) = await _service.GetPagedAsync(page, pageSize);

            var result = new
            {
                data = transfers,
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
            var transfer = await _service.GetByIdAsync(id);
            if (transfer == null)
                return NotFound(new { message = "Không tìm thấy bản ghi chuyển trường." });

            return Ok(transfer);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] SchoolTransferHistoryCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();

            void AddError(string key, string message)
            {
                if (!errors.ContainsKey(key))
                    errors[key] = new List<string>();
                errors[key].Add(message);
            }

            // Validate các trường bắt buộc
            if (!dto.Studentid.HasValue)
                AddError("Studentid", "Học sinh không được để trống.");
            if (!dto.Fromschoolinfoid.HasValue)
                AddError("Fromschoolinfoid", "Trường chuyển đi không được để trống.");
            if (!dto.Toschoolinfoid.HasValue)
                AddError("Toschoolinfoid", "Trường chuyển đến không được để trống.");
            if (!dto.Semesterid.HasValue)
                AddError("Semesterid", "Học kỳ không được để trống.");
            if (!dto.Fromclassid.HasValue)
                AddError("Fromclassid", "Lớp chuyển đi không được để trống.");
            if (!dto.Toclassid.HasValue)
                AddError("Toclassid", "Lớp chuyển đến không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Transferdate))
                AddError("Transferdate", "Ngày chuyển không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Reason))
                AddError("Reason", "Lý do không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Transfertype))
                AddError("Transfertype", "Loại chuyển không được để trống.");

            // Parse ngày
            DateOnly parsedDate;
            if (!string.IsNullOrWhiteSpace(dto.Transferdate) &&
                !DateOnly.TryParse(dto.Transferdate, out parsedDate))
            {
                AddError("Transferdate", "Ngày chuyển không đúng định dạng yyyy-MM-dd.");
            }

            // Validate file upload (nếu có)
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
                return BadRequest(new
                {
                    message = "Dữ liệu không hợp lệ",
                    errors
                });
            }

            // Xử lý upload file
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

            // Tạo entity
            var entity = new Schooltransferhistory
            {
                Studentid = dto.Studentid!.Value,
                Fromschoolinfoid = dto.Fromschoolinfoid!.Value,
                Toschoolinfoid = dto.Toschoolinfoid!.Value,
                Semesterid = dto.Semesterid!.Value,
                Fromclassid = dto.Fromclassid!.Value,
                Toclassid = dto.Toclassid!.Value,
                Transferdate = parsedDate,
                Reason = dto.Reason,
                Transfertype = dto.Transfertype,
                Attachmenturl = filePath
            };

            await _service.CreateAsync(entity);
            return Ok(new { message = "Tạo bản ghi chuyển trường thành công." });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateTransfer(int id, [FromForm] SchoolTransferHistoryCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            if (dto.Studentid == null)
                AddError("Studentid", "Không được để trống mã sinh viên.");
            if (dto.Fromschoolinfoid == null)
                AddError("Fromschoolinfoid", "Không được để trống trường đi.");
            if (dto.Toschoolinfoid == null)
                AddError("Toschoolinfoid", "Không được để trống trường đến.");
            if (dto.Semesterid == null)
                AddError("Semesterid", "Không được để trống học kỳ.");
            if (string.IsNullOrWhiteSpace(dto.Transfertype))
                AddError("Transfertype", "Không được để trống loại chuyển.");
            if (string.IsNullOrWhiteSpace(dto.Transferdate))
                AddError("Transferdate", "Không được để trống ngày chuyển.");

            // Validate ngày
            DateOnly parsedDate;
            if (!string.IsNullOrWhiteSpace(dto.Transferdate) &&
                !DateOnly.TryParse(dto.Transferdate, out parsedDate))
            {
                AddError("Transferdate", "Ngày chuyển không đúng định dạng yyyy-MM-dd.");
            }

            // Validate file upload (nếu có)
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

            if (dto.Attachmentfile is { Length: > 0 })
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.Attachmentfile.FileName}";
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await dto.Attachmentfile.CopyToAsync(stream);

                filePath = $"/uploads/{fileName}";
            }

            // Cập nhật thông tin
            existing.Studentid = dto.Studentid!.Value;
            existing.Fromschoolinfoid = dto.Fromschoolinfoid!.Value;
            existing.Toschoolinfoid = dto.Toschoolinfoid!.Value;
            existing.Semesterid = dto.Semesterid!.Value;
            existing.Transferdate = parsedDate;
            existing.Transfertype = dto.Transfertype;
            existing.Reason = dto.Reason;
            existing.Fromclassid = dto.Fromclassid;
            existing.Toclassid = dto.Toclassid;
            existing.Attachmenturl = filePath;

            await _service.UpdateAsync(id, existing);
            return Ok(new { message = "Cập nhật chuyển trường thành công." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
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
