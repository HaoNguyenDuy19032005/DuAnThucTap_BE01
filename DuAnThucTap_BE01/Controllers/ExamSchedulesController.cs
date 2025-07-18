using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamSchedulesController : ControllerBase
    {
        private readonly IExamScheduleService _service;

        public ExamSchedulesController(IExamScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedExamSchedules(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagedResult = await _service.GetPagedExamSchedulesAsync(searchQuery, pageNumber, pageSize);
            return Ok(new ApiResponse<PagedResponse<ExamScheduleResponseDto>>((int)HttpStatusCode.OK, "Lấy danh sách lịch thi thành công", pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var examSchedule = await _service.GetByIdAsync(id);
            if (examSchedule == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch thi với ID = {id}", null));
            }
            return Ok(new ApiResponse<ExamScheduleResponseDto>((int)HttpStatusCode.OK, "Lấy thông tin lịch thi thành công", examSchedule));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamScheduleDto examScheduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var created = await _service.CreateAsync(examScheduleDto);

                // --- SỬA LỖI: Lấy lại thông tin chi tiết để trả về ---
                var resultDto = await _service.GetByIdAsync(created.Examscheduleid);

                var response = new ApiResponse<ExamScheduleResponseDto>((int)HttpStatusCode.Created, "Tạo lịch thi thành công", resultDto);
                return CreatedAtAction(nameof(GetById), new { id = created.Examscheduleid }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExamScheduleDto examScheduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var result = await _service.UpdateAsync(id, examScheduleDto);
                if (result == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch thi với ID = {id}", null));
                }

                // --- SỬA LỖI: Lấy lại thông tin chi tiết để trả về ---
                var resultDto = await _service.GetByIdAsync(id);
                return Ok(new ApiResponse<ExamScheduleResponseDto>((int)HttpStatusCode.OK, "Cập nhật thành công", resultDto));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch thi với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}
