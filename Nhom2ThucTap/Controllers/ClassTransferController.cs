using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Services;
using System.IO; 
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassTransferController : ControllerBase
    {
        private readonly IClassTransferService _service;

        public ClassTransferController(IClassTransferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transfers = await _service.GetAllAsync();
            return Ok(transfers);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "page và pageSize phải lớn hơn 0." });

            var (transfers, totalCount) = await _service.GetPagedAsync(page, pageSize);

            return Ok(new
            {
                data = transfers,
                pagination = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalItems = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transfer = await _service.GetByIdAsync(id);
            if (transfer == null)
                return NotFound(new { message = "Không tìm thấy bản ghi." });

            return Ok(transfer);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Transfer([FromForm] ClasstransferhistoryCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            if (dto.Studentid == null)
                AddError("Studentid", "Mã sinh viên không được để trống.");
            if (dto.Fromclassid == null)
                AddError("Fromclassid", "Mã lớp cũ không được để trống.");
            if (dto.Toclassid == null)
                AddError("Toclassid", "Mã lớp mới không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Reason))
                AddError("Reason", "Lý do không được để trống.");

            if (dto.Studentid != null && !await _service.ExistsStudentAsync(dto.Studentid.Value))
                AddError("Studentid", "Mã sinh viên không tồn tại.");
            if (dto.Fromclassid != null && !await _service.ExistsClassAsync(dto.Fromclassid.Value))
                AddError("Fromclassid", "Mã lớp cũ không tồn tại.");
            if (dto.Toclassid != null && !await _service.ExistsClassAsync(dto.Toclassid.Value))
                AddError("Toclassid", "Mã lớp mới không tồn tại.");

            // Kiểm tra file upload nếu có
            if (dto.Attachmentfile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var extension = Path.GetExtension(dto.Attachmentfile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    AddError("Attachmentfile", "Chỉ cho phép gửi file ảnh (jpg, jpeg, png, gif, bmp).");
                }
            }

            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            try
            {
                await _service.TransferAndUpdateStatusAsync(dto);
                return Ok(new { message = "Chuyển lớp thành công." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] ClasstransferhistoryCreateDto dto)
        {
            var errors = new Dictionary<string, List<string>>();
            void AddError(string field, string message)
            {
                if (!errors.ContainsKey(field)) errors[field] = new();
                errors[field].Add(message);
            }

            if (dto.Studentid == null)
                AddError("Studentid", "Mã sinh viên không được để trống.");
            if (dto.Fromclassid == null)
                AddError("Fromclassid", "Mã lớp cũ không được để trống.");
            if (dto.Toclassid == null)
                AddError("Toclassid", "Mã lớp mới không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.Reason))
                AddError("Reason", "Lý do không được để trống.");

            if (dto.Studentid != null && !await _service.ExistsStudentAsync(dto.Studentid.Value))
                AddError("Studentid", "Mã sinh viên không tồn tại.");
            if (dto.Fromclassid != null && !await _service.ExistsClassAsync(dto.Fromclassid.Value))
                AddError("Fromclassid", "Mã lớp cũ không tồn tại.");
            if (dto.Toclassid != null && !await _service.ExistsClassAsync(dto.Toclassid.Value))
                AddError("Toclassid", "Mã lớp mới không tồn tại.");

            // Kiểm tra file upload nếu có
            if (dto.Attachmentfile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var extension = Path.GetExtension(dto.Attachmentfile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    AddError("Attachmentfile", "Chỉ cho phép gửi file ảnh (jpg, jpeg, png, gif, bmp).");
                }
            }

            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            try
            {
                await _service.UpdateTransferAsync(id, dto);
                return Ok(new { message = "Cập nhật thông tin chuyển lớp thành công." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteTransferAsync(id);
                return Ok(new { message = "Xóa thành công." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.Message });
            }
        }
    }
}
