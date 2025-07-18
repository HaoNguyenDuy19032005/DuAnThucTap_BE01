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
    public class ExamGradersController : ControllerBase
    {
        private readonly IExamGraderService _service;

        public ExamGradersController(IExamGraderService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedExamGraders(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagedResult = await _service.GetPagedExamGradersAsync(searchQuery, pageNumber, pageSize);
            return Ok(new ApiResponse<PagedResponse<ExamGraderResponseDto>>((int)HttpStatusCode.OK, "Lấy danh sách người chấm thi thành công", pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var examGrader = await _service.GetByIdAsync(id);
            if (examGrader == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy người chấm thi với ID = {id}", null));
            }
            return Ok(new ApiResponse<ExamGraderResponseDto>((int)HttpStatusCode.OK, "Lấy thông tin người chấm thi thành công", examGrader));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamGraderDto examGraderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            // --- SỬA LỖI: Thêm try-catch để xử lý lỗi trùng lặp ---
            try
            {
                var created = await _service.CreateAsync(examGraderDto);
                var response = new ApiResponse<Examgrader>((int)HttpStatusCode.Created, "Tạo người chấm thi thành công", created);
                return CreatedAtAction(nameof(GetById), new { id = created.Examgraderid }, response);
            }
            catch (InvalidOperationException ex)
            {
                // Trả về lỗi 400 với thông báo từ Service
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExamGraderDto examGraderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            // --- SỬA LỖI: Thêm try-catch để xử lý lỗi trùng lặp ---
            try
            {
                var result = await _service.UpdateAsync(id, examGraderDto);
                if (result == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy người chấm thi với ID = {id}", null));
                }
                return Ok(new ApiResponse<Examgrader>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
            }
            catch (InvalidOperationException ex)
            {
                // Trả về lỗi 400 với thông báo từ Service
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy người chấm thi với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}
