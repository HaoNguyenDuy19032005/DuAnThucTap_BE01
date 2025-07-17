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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherRequestDto teacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                // Gọi service không có tham số file
                var createdTeacher = await _service.CreateAsync(teacherDto);
                var response = new ApiResponse<Teacher>((int)HttpStatusCode.Created, "Tạo giáo viên thành công", createdTeacher);
                return CreatedAtAction(nameof(GetById), new { id = createdTeacher.Teacherid }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        // Dùng [FromBody] và không còn IFormFile
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherRequestDto teacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                // Gọi service không có tham số file
                var result = await _service.UpdateAsync(id, teacherDto);
                if (result == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy giáo viên với ID = {id}", null));
                }
                return Ok(new ApiResponse<Teacher>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        // Action MỚI chỉ để upload ảnh
        [HttpPost("{id}/avatar")]
        public async Task<IActionResult> UploadAvatar(int id, IFormFile avatarFile)
        {
            if (avatarFile == null || avatarFile.Length == 0)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Vui lòng chọn một file ảnh.", null));
            }

            try
            {
                var newAvatarUrl = await _service.UpdateAvatarAsync(id, avatarFile);
                if (newAvatarUrl == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy giáo viên với ID = {id}", null));
                }
                return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Cập nhật ảnh đại diện thành công", new { avatarUrl = newAvatarUrl }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<TeacherDto>>((int)HttpStatusCode.OK, "Lấy danh sách giáo viên thành công", teachers));
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