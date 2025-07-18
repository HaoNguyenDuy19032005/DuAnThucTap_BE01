using System.Net;
using System.Threading.Tasks;
using DuAnThucTap_BE01.Dto;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _service;

        public ExamsController(IExamService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedExams(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagedResult = await _service.GetPagedExamsAsync(searchQuery, pageNumber, pageSize);
            return Ok(new ApiResponse<PagedResponse<ExamResponseDto>>((int)HttpStatusCode.OK, "Lấy danh sách kỳ thi thành công", pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exam = await _service.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy kỳ thi với ID = {id}", null));
            }
            return Ok(new ApiResponse<ExamResponseDto>((int)HttpStatusCode.OK, "Lấy thông tin kỳ thi thành công", exam));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamDto examDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var exam = new Exam
            {
                Schoolyearid = examDto.Schoolyearid,
                Gradelevelid = examDto.Gradelevelid,
                Semesterid = examDto.Semesterid,
                Subjectid = examDto.Subjectid,
                Examname = examDto.Examname,
                Examdate = examDto.Examdate,
                Durationminutes = examDto.Durationminutes,
                Classtypeid = examDto.Classtypeid,
                Graderassignmenttypeid = examDto.Graderassignmenttypeid
            };

            var created = await _service.CreateAsync(exam);

            // --- SỬA LỖI: Lấy lại thông tin chi tiết để trả về ---
            // Sau khi tạo, gọi lại GetByIdAsync để lấy ExamResponseDto có đầy đủ tên
            var resultDto = await _service.GetByIdAsync(created.Examid);

            var response = new ApiResponse<ExamResponseDto>((int)HttpStatusCode.Created, "Tạo kỳ thi thành công", resultDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Examid }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExamDto examDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var examToUpdate = new Exam
            {
                Examid = id,
                Schoolyearid = examDto.Schoolyearid,
                Gradelevelid = examDto.Gradelevelid,
                Semesterid = examDto.Semesterid,
                Subjectid = examDto.Subjectid,
                Examname = examDto.Examname,
                Examdate = examDto.Examdate,
                Durationminutes = examDto.Durationminutes,
                Classtypeid = examDto.Classtypeid,
                Graderassignmenttypeid = examDto.Graderassignmenttypeid
            };

            var result = await _service.UpdateAsync(id, examToUpdate);
            if (result == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy kỳ thi với ID = {id}", null));
            }

            // --- SỬA LỖI: Lấy lại thông tin chi tiết để trả về ---
            var resultDto = await _service.GetByIdAsync(id);
            return Ok(new ApiResponse<ExamResponseDto>((int)HttpStatusCode.OK, "Cập nhật thành công", resultDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy kỳ thi với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}
