using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinetypeController : ControllerBase
    {
        private readonly IDisciplinetypeService _service;

        public DisciplinetypeController(IDisciplinetypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "page và pageSize phải lớn hơn 0." });

            var (data, total) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new
            {
                message = "Lấy danh sách thành công",
                total,
                page,
                pageSize,
                data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy loại kỷ luật." });
            return Ok(new { message = "Lấy dữ liệu thành công", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Disciplinetype disciplinetype)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.AddAsync(disciplinetype);
                return Ok(new { message = "Tạo bản ghi thành công", data = created });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(400, new { message = $"Lỗi cơ sở dữ liệu: {ex.InnerException?.Message ?? ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi server: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Disciplinetype disciplinetype)
        {
            if (!ModelState.IsValid || id != disciplinetype.Disciplinetypeid)
                return BadRequest(new { message = "ID không khớp hoặc dữ liệu không hợp lệ." });

            try
            {
                var updated = await _service.UpdateAsync(id, disciplinetype);
                if (updated == null)
                    return NotFound(new { message = "Không tìm thấy bản ghi." });

                return Ok(new { message = "Cập nhật thành công", data = updated });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Không tìm thấy bản ghi để xoá." });

                return Ok(new { message = "Xoá bản ghi thành công" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(400, new { message = "Không thể xoá do liên quan dữ liệu khác." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}
