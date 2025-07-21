using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentTransferReceiptController : ControllerBase
    {
        private readonly IStudentTransferReceiptService _service;

        public StudentTransferReceiptController(IStudentTransferReceiptService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new { message = "Lấy danh sách thành công", data = result });
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "page và pageSize phải lớn hơn 0" });
            }

            var (items, totalCount) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new
            {
                message = "Lấy danh sách có phân trang thành công",
                data = items,
                totalItems = totalCount,
                currentPage = page,
                pageSize = pageSize,
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var receipt = await _service.GetByIdAsync(id);
            if (receipt == null)
                return NotFound(new { message = "Không tìm thấy bản ghi" });

            return Ok(new { message = "Lấy bản ghi thành công", data = receipt });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] StudentTransferReceiptCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            // Validate các trường dữ liệu
            if (string.IsNullOrWhiteSpace(dto.StudentName))
                AddError("StudentName", "Họ tên học sinh không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.StudentCode))
                AddError("StudentCode", "Mã học sinh không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.TransferDate))
                AddError("TransferDate", "Ngày chuyển trường không được để trống.");

            if (dto.SemesterId == null)
                AddError("SemesterId", "Học kỳ không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Province))
                AddError("Province", "Tỉnh không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.District))
                AddError("District", "Huyện không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.FromSchool))
                AddError("FromSchool", "Tên trường chuyển đến không được để trống.");

            // Bắt lỗi file đính kèm
            if (dto.AttachmentFile == null || dto.AttachmentFile.Length == 0)
            {
                AddError("AttachmentFile", "File đính kèm không được để trống.");
            }
            else
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var ext = Path.GetExtension(dto.AttachmentFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(ext))
                {
                    AddError("AttachmentFile", "Chỉ cho phép các định dạng file: jpg, jpeg, png, gif.");
                }

                var maxFileSize = 5 * 1024 * 1024; // 5MB
                if (dto.AttachmentFile.Length > maxFileSize)
                {
                    AddError("AttachmentFile", "Kích thước file không được vượt quá 5MB.");
                }
            }

            // Parse date
            DateTime? transferDateParsed = null;
            if (!string.IsNullOrWhiteSpace(dto.TransferDate))
            {
                if (!DateTime.TryParse(dto.TransferDate, out var parsedDate))
                    AddError("TransferDate", "Ngày chuyển trường không hợp lệ (định dạng yyyy-MM-dd).");
                else
                    transferDateParsed = parsedDate;
            }

            // Kiểm tra tồn tại học kỳ
            if (dto.SemesterId != null && !await _service.ExistsSemesterAsync(dto.SemesterId.Value))
                AddError("SemesterId", "Học kỳ không tồn tại.");

            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            // Lưu file
            var fileName = Guid.NewGuid() + Path.GetExtension(dto.AttachmentFile!.FileName);
            var filePath = Path.Combine("Uploads", "TransferReceipts", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.AttachmentFile.CopyToAsync(stream);
            }

            var receipt = new StudentTransferReceipt
            {
                StudentName = dto.StudentName!,
                StudentCode = dto.StudentCode!,
                TransferDate = transferDateParsed,
                SemesterId = dto.SemesterId,
                Province = dto.Province!,
                District = dto.District!,
                FromSchool = dto.FromSchool!,
                Reason = dto.Reason,
                AttachmentFile = fileName
            };

            var created = await _service.CreateAsync(receipt);
            return Ok(new { message = "Tạo bản ghi thành công", data = created });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] StudentTransferReceiptCreateDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật" });

            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            // Validate các trường dữ liệu (giống Create)
            if (string.IsNullOrWhiteSpace(dto.StudentName))
                AddError("StudentName", "Họ tên học sinh không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.StudentCode))
                AddError("StudentCode", "Mã học sinh không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.TransferDate))
                AddError("TransferDate", "Ngày chuyển trường không được để trống.");

            if (dto.SemesterId == null)
                AddError("SemesterId", "Học kỳ không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Province))
                AddError("Province", "Tỉnh không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.District))
                AddError("District", "Huyện không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.FromSchool))
                AddError("FromSchool", "Tên trường chuyển đến không được để trống.");

            // Validate file đính kèm nếu có gửi lên mới check
            if (dto.AttachmentFile != null && dto.AttachmentFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var ext = Path.GetExtension(dto.AttachmentFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(ext))
                {
                    AddError("AttachmentFile", "Chỉ cho phép các định dạng file: jpg, jpeg, png, gif.");
                }

                var maxFileSize = 5 * 1024 * 1024; // 5MB
                if (dto.AttachmentFile.Length > maxFileSize)
                {
                    AddError("AttachmentFile", "Kích thước file không được vượt quá 5MB.");
                }
            }

            // Parse date
            DateTime? transferDateParsed = null;
            if (!string.IsNullOrWhiteSpace(dto.TransferDate))
            {
                if (!DateTime.TryParse(dto.TransferDate, out var parsedDate))
                    AddError("TransferDate", "Ngày chuyển trường không hợp lệ (định dạng yyyy-MM-dd).");
                else
                    transferDateParsed = parsedDate;
            }

            // Kiểm tra tồn tại học kỳ
            if (dto.SemesterId != null && !await _service.ExistsSemesterAsync(dto.SemesterId.Value))
                AddError("SemesterId", "Học kỳ không tồn tại.");

            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            // Lưu file nếu có
            if (dto.AttachmentFile != null && dto.AttachmentFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.AttachmentFile.FileName);
                var filePath = Path.Combine("Uploads", "TransferReceipts", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.AttachmentFile.CopyToAsync(stream);
                existing.AttachmentFile = fileName;
            }

            // Cập nhật các trường
            existing.StudentName = dto.StudentName!;
            existing.StudentCode = dto.StudentCode!;
            existing.TransferDate = transferDateParsed;
            existing.SemesterId = dto.SemesterId;
            existing.Province = dto.Province!;
            existing.District = dto.District!;
            existing.FromSchool = dto.FromSchool!;
            existing.Reason = dto.Reason;

            var success = await _service.UpdateAsync(id, existing);
            return success
                ? Ok(new { message = "Cập nhật thành công" })
                : StatusCode(500, new { message = "Cập nhật thất bại" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success
                ? Ok(new { message = "Xoá bản ghi thành công" })
                : NotFound(new { message = "Không tìm thấy bản ghi để xoá" });
        }
    }
}
