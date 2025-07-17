using DuAnThucTap_BE01.DTO;
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
    public class TeacherConcurrentSubjectsController : ControllerBase
    {
        private readonly ITeacherConcurrentSubjectService _service;
        public TeacherConcurrentSubjectsController(ITeacherConcurrentSubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            var response = new ApiResponse<IEnumerable<TeacherConcurrentSubjectDto>>((int)HttpStatusCode.OK, "Lấy danh sách thành công", data);
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
        public async Task<IActionResult> Create([FromBody] Teacherconcurrentsubject assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var result = await _service.CreateAsync(assignment);

            if (!result.Succeeded)
            {
                // Lỗi trùng lặp
                return Conflict(new ApiResponse<object>((int)HttpStatusCode.Conflict, result.ErrorMessage!, null));
            }

            var response = new ApiResponse<Teacherconcurrentsubject>((int)HttpStatusCode.Created, "Tạo phân công thành công", assignment);
            return CreatedAtAction(nameof(GetById),
                new { teacherId = assignment.Teacherid, subjectId = assignment.Subjectid, schoolYearId = assignment.Schoolyearid },
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