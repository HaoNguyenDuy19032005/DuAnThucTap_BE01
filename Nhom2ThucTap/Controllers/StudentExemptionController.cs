using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExemptionController : ControllerBase
    {
        private readonly IStudentExemptionService _service;

        public StudentExemptionController(IStudentExemptionService service)
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

            var (exemptions, totalCount) = await _service.GetPagedAsync(page, pageSize);

            var result = new
            {
                data = exemptions,
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
            var exemption = await _service.GetByIdAsync(id);
            if (exemption == null)
                return NotFound(new { message = "Không tìm thấy bản ghi miễn giảm." });

            return Ok(exemption);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Studentexemption exemption)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
            }

            try
            {
                await _service.AddAsync(exemption);
                return Ok(new { message = "Tạo bản ghi thành công." });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi cơ sở dữ liệu. Có thể bản ghi liên kết không tồn tại.",
                    detail = dbEx.InnerException?.Message ?? dbEx.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Studentexemption exemption)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
            }

            try
            {
                var updated = await _service.UpdateAsync(id, exemption);
                if (updated == null)
                    return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật." });

                return Ok(new { message = "Cập nhật bản ghi thành công." });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi cơ sở dữ liệu khi cập nhật.",
                    detail = dbEx.InnerException?.Message ?? dbEx.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.Message });
            }
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
