using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsOfExemptionController : ControllerBase
    {
        private readonly ISubjectsOfExemptionService _service;

        public SubjectsOfExemptionController(ISubjectsOfExemptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(new { message = "Lấy danh sách thành công", data = list });
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "page và pageSize phải lớn hơn 0" });

            var (items, total) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new
            {
                message = "Lấy danh sách phân trang thành công",
                data = items,
                totalItems = total,
                currentPage = page,
                pageSize,
                totalPages = (int)Math.Ceiling((double)total / pageSize)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { message = "Không tìm thấy bản ghi" });

            return Ok(new { message = "Lấy bản ghi thành công", data = item });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Subjectsofexemption exemption)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
            }

            var created = await _service.AddAsync(exemption);
            return Ok(new { message = "Tạo bản ghi thành công", data = created });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Subjectsofexemption exemption)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
            }

            var updated = await _service.UpdateAsync(id, exemption);
            if (updated == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật" });

            return Ok(new { message = "Cập nhật thành công", data = updated });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Không tìm thấy bản ghi để xoá" });

            return Ok(new { message = "Xoá bản ghi thành công" });
        }
    }
}
