using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DuAnThucTap.Model;

namespace DuAnThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            var result = await _service.GetAllAsync(search, page, pageSize);

            if (result.Count == 0)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy môn học phù hợp.",
                    search = search,
                    page = page
                });
            }

            return Ok(new
            {
                message = "Lấy danh sách môn học thành công",
                currentPage = result.PageIndex,
                totalPages = result.TotalPage,
                data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _service.GetByIdAsync(id);
            if (subject == null)
                return NotFound(new { message = "Không tìm thấy môn học." });

            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> PostSubject(Subject subject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(subject);
                return Ok(new
                {
                    message = "Tạo môn học thành công.",
                    data = created
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi tạo môn học." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _service.UpdateAsync(id, subject);
                if (!updated)
                    return NotFound(new { message = "Không tìm thấy môn học để cập nhật." });

                return Ok(new { message = "Cập nhật môn học thành công." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi cập nhật môn học." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Không tìm thấy môn học để xóa." });

                return Ok(new { message = "Xóa môn học thành công." });
            }
            catch
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xóa môn học." });
            }
        }
    }

}
