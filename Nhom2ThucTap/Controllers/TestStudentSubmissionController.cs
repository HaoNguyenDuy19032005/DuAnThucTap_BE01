using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.DTOs;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Service;

namespace Nhom2ThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestStudentSubmissionController : ControllerBase
    {
        private readonly ITestStudentSubmissionService _service;

        public TestStudentSubmissionController(ITestStudentSubmissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? studentId = null,
            [FromQuery] int? testId = null)
        {
            var result = await _service.GetAllAsync(page, pageSize, studentId, testId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var submission = await _service.GetByIdAsync(id);
            if (submission == null)
            {
                return NotFound(new { message = $"Không tìm thấy bài nộp với ID = {id}" });
            }

            return Ok(submission);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestStudentSubmissionDto dto)
        {
            // Bắt lỗi thủ công
            if (dto.TestId == null || dto.TestId <= 0)
                return BadRequest(new { message = "TestId không được để trống hoặc <= 0." });

            if (dto.StudentId == null || dto.StudentId <= 0)
                return BadRequest(new { message = "StudentId không được để trống hoặc <= 0." });

            if (dto.StartTime.HasValue && dto.SubmissionTime.HasValue)
            {
                if (dto.SubmissionTime <= dto.StartTime)
                    return BadRequest(new { message = "SubmissionTime phải sau StartTime." });
            }

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.SubmissionId }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi tạo mới: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TestStudentSubmissionDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID không hợp lệ." });

            if (dto.StartTime.HasValue && dto.SubmissionTime.HasValue)
            {
                if (dto.SubmissionTime <= dto.StartTime)
                    return BadRequest(new { message = "SubmissionTime phải sau StartTime." });
            }

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
            {
                return NotFound(new { message = $"Không tìm thấy bài nộp với ID = {id}" });
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID không hợp lệ." });

            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Không tìm thấy bài nộp với ID = {id}" });
            }

            return NoContent();
        }
    }
}
