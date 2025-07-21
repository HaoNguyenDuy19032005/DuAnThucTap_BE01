using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSemesterSummaryController : ControllerBase
    {
        private readonly IStudentSemesterSummaryService _service;

        public StudentSemesterSummaryController(IStudentSemesterSummaryService service)
        {
            _service = service;
        }

        // ✅ Xem tất cả
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // ✅ Xem theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var summary = await _service.GetByIdAsync(id);
            if (summary == null)
                return NotFound(new { message = "Không tìm thấy bản ghi tổng kết học kỳ." });

            return Ok(summary);
        }

        // ✅ Phân trang
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Giá trị page và pageSize phải lớn hơn 0." });

            var (data, total) = await _service.GetPagedAsync(page, pageSize);

            return Ok(new
            {
                data = data,
                pagination = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalItems = total,
                    totalPages = (int)Math.Ceiling((double)total / pageSize)
                }
            });
        }

        // ❌ Tạm thời ẩn tạo mới
        /*
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Studentsemestersummary summary)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            summary.Averagescore = null;
            summary.Performancerating = null;
            summary.Conductrating = null;
            summary.Calculateddate = null;

            try
            {
                await _service.AddAsync(summary);
                return Ok(new { message = "Tạo bản ghi thành công. Điểm sẽ được tính tự động." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.Message });
            }
        }
        */

        // ❌ Tạm thời ẩn cập nhật
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Studentsemestersummary summary)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != summary.Summaryid)
                return BadRequest(new { message = "ID không khớp." });

            summary.Averagescore = null;
            summary.Performancerating = null;
            summary.Conductrating = null;
            summary.Calculateddate = null;

            try
            {
                var updated = await _service.UpdateAsync(id, summary);
                if (updated == null)
                    return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật." });

                return Ok(new { message = "Cập nhật bản ghi thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.Message });
            }
        }
        */

        // ❌ Tạm thời ẩn xóa
        /*
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
        */
    }
}
