using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentYearlyStatusController : ControllerBase
    {
        private readonly IStudentYearlyStatusService _service;

        public StudentYearlyStatusController(IStudentYearlyStatusService service)
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

            var (items, totalCount) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new
            {
                message = "Lấy danh sách phân trang thành công",
                data = items,
                totalItems = totalCount,
                currentPage = page,
                pageSize,
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy bản ghi" });
            return Ok(new { message = "Lấy bản ghi thành công", data = result });
        }

        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            var result = await _service.GetByStudentIdAsync(studentId);
            if (result == null) return NotFound(new { message = "Không tìm thấy bản ghi của học sinh" });
            return Ok(new { message = "Lấy dữ liệu học sinh thành công", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Studentyearlystatus status)
        {
            // Xóa các navigation để tránh lỗi modelstate không hợp lệ
            ModelState.Remove("Class");
            ModelState.Remove("Student");
            ModelState.Remove("Gradelevel");
            ModelState.Remove("Schoolyear");

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

            // Kiểm tra khoá ngoại
            if (!await _service.IsValidForeignKeysAsync(status))
            {
                return BadRequest(new
                {
                    message = "Một trong các khoá ngoại không hợp lệ (Student, Class, GradeLevel, SchoolYear)"
                });
            }

            await _service.AddAsync(status);
            return Ok(new { message = "Tạo bản ghi thành công", data = status });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Studentyearlystatus status)
        {
            if (id != status.Studentyearlystatusid)
                return BadRequest(new { message = "ID không khớp với bản ghi cần cập nhật" });

            ModelState.Remove("Class");
            ModelState.Remove("Student");
            ModelState.Remove("Gradelevel");
            ModelState.Remove("Schoolyear");

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

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật" });

            if (!await _service.IsValidForeignKeysAsync(status))
            {
                return BadRequest(new
                {
                    message = "Một trong các khoá ngoại không hợp lệ (Student, Class, GradeLevel, SchoolYear)"
                });
            }

            await _service.UpdateAsync(status);
            return Ok(new { message = "Cập nhật bản ghi thành công", data = status });
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
