
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net; // Cần cho HttpStatusCode

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeachersController(ITeacherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _service.GetAllAsync();
            var response = new ApiResponse<IEnumerable<TeacherDto>>(
                (int)HttpStatusCode.OK,
                "Lấy danh sách giáo viên thành công",
                teachers
            );
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _service.GetByIdAsync(id);
            if (teacher == null)
            {
                var notFoundResponse = new ApiResponse<TeacherDto?>(
                    (int)HttpStatusCode.NotFound,
                    $"Không tìm thấy giáo viên với ID = {id}",
                    null
                );
                return NotFound(notFoundResponse);
            }

            var successResponse = new ApiResponse<TeacherDto>(
                (int)HttpStatusCode.OK,
                "Lấy thông tin giáo viên thành công",
                teacher
            );
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                // Trả về lỗi validation
                var errorResponse = new ApiResponse<object>(
                    (int)HttpStatusCode.BadRequest,
                    "Dữ liệu không hợp lệ",
                    ModelState
                );
                return BadRequest(errorResponse);
            }

            var createdTeacher = await _service.CreateAsync(teacher);
            var response = new ApiResponse<Teacher>(
                (int)HttpStatusCode.Created,
                "Tạo giáo viên thành công",
                createdTeacher
            );

            return CreatedAtAction(nameof(GetById), new { id = createdTeacher.Teacherid }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.Teacherid)
            {
                var badRequestResponse = new ApiResponse<object>(
                   (int)HttpStatusCode.BadRequest,
                   "ID trong URL và ID trong body không khớp",
                   null
               );
                return BadRequest(badRequestResponse);
            }

            var result = await _service.UpdateAsync(id, teacher);
            if (result == null)
            {
                var notFoundResponse = new ApiResponse<Teacher?>(
                    (int)HttpStatusCode.NotFound,
                    $"Không tìm thấy giáo viên với ID = {id} để cập nhật",
                    null
                );
                return NotFound(notFoundResponse);
            }

            var successResponse = new ApiResponse<Teacher>(
                (int)HttpStatusCode.OK,
                "Cập nhật thông tin giáo viên thành công",
                result
            );
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                var notFoundResponse = new ApiResponse<object>(
                    (int)HttpStatusCode.NotFound,
                    $"Không tìm thấy giáo viên với ID = {id} để xóa",
                    null
                );
                return NotFound(notFoundResponse);
            }

            var successResponse = new ApiResponse<object>(
                (int)HttpStatusCode.OK,
                "Xóa giáo viên thành công",
                null
            );
            return Ok(successResponse);
        }
    }
}