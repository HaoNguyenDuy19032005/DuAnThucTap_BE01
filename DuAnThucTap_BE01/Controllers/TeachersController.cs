using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> GetAll(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagedResult = await _service.GetAllAsync(searchQuery, pageNumber, pageSize);
            return Ok(new ApiResponse<PagedResponse<TeacherDto>>((int)HttpStatusCode.OK, "Lấy danh sách giáo viên thành công", pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _service.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy giáo viên với ID = {id}", null));
            }
            return Ok(new ApiResponse<TeacherDto>((int)HttpStatusCode.OK, "Lấy thông tin giáo viên thành công", teacher));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
             [FromForm] TeacherRequestDto teacherDto,
             [FromForm] IFormFile? avatarFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var createdTeacher = await _service.CreateAsync(teacherDto, avatarFile);
            var response = new ApiResponse<Teacher>((int)HttpStatusCode.Created, "Tạo giáo viên thành công", createdTeacher);
            return CreatedAtAction(nameof(GetById), new { id = createdTeacher.Teacherid }, response);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] TeacherRequestDto teacherDto,
            [FromForm] IFormFile? avatarFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var result = await _service.UpdateAsync(id, teacherDto, avatarFile);
            if (result == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy giáo viên với ID = {id}", null));
            }
            return Ok(new ApiResponse<Teacher>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy giáo viên với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}