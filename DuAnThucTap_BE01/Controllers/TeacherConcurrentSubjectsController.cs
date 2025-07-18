using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherConcurrentSubjectsController : ControllerBase
    {
        private readonly ITeacherConcurrentSubjectService _service;
        public TeacherConcurrentSubjectsController(ITeacherConcurrentSubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var data = await _service.GetAllAsync(searchQuery, pageNumber, pageSize);
            var response = new ApiResponse<PagedResponse<TeacherConcurrentSubjectDto>>((int)HttpStatusCode.OK, "Lấy danh sách thành công", data);
            return Ok(response);
        }

        [HttpGet("{teacherId}/{subjectId}/{schoolYearId}")]
        public async Task<IActionResult> GetById(int teacherId, int subjectId, int schoolYearId)
        {
            var data = await _service.GetByIdAsync(teacherId, subjectId, schoolYearId);
            if (data == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, "Không tìm thấy phân công này", null));
            }
            return Ok(new ApiResponse<TeacherConcurrentSubjectDto>((int)HttpStatusCode.OK, "Lấy dữ liệu thành công", data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherConcurrentSubjectRequestDto assignmentDto) // Thay đổi tham số
        {
            // ModelState.IsValid sẽ tự động kiểm tra các Data Annotations trong TeacherConcurrentSubjectRequestDto
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var result = await _service.CreateAsync(assignmentDto); // Gọi service với DTO request

            if (!result.Succeeded)
            {
                // Lỗi trùng lặp hoặc các lỗi khác từ service
                return Conflict(new ApiResponse<object>((int)HttpStatusCode.Conflict, result.ErrorMessage!, null));
            }

            // Dùng CreatedAssignment từ kết quả của service để lấy khóa chính phức hợp
            var response = new ApiResponse<Teacherconcurrentsubject>((int)HttpStatusCode.Created, "Tạo phân công thành công", result.CreatedAssignment);
            return CreatedAtAction(nameof(GetById),
                new { teacherId = result.CreatedAssignment!.Teacherid, subjectId = result.CreatedAssignment.Subjectid, schoolYearId = result.CreatedAssignment.Schoolyearid },
                response);
        }

        [HttpDelete("{teacherId}/{subjectId}/{schoolYearId}")]
        public async Task<IActionResult> Delete(int teacherId, int subjectId, int schoolYearId)
        {
            var success = await _service.DeleteAsync(teacherId, subjectId, schoolYearId);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, "Không tìm thấy phân công để xóa", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa phân công thành công", null));
        }
    }
}