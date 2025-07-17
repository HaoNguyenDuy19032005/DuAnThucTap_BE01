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
    public class TeacherWorkHistoriesController : ControllerBase
    {
        private readonly ITeacherWorkHistoryService _service;
        public TeacherWorkHistoriesController(ITeacherWorkHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<TeacherWorkHistoryDto>>((int)HttpStatusCode.OK, "Lấy danh sách thành công", data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử công tác với ID = {id}", null));
            }
            return Ok(new ApiResponse<TeacherWorkHistoryDto>((int)HttpStatusCode.OK, "Lấy dữ liệu thành công", data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Teacherworkhistory history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }
            var created = await _service.CreateAsync(history);
            var response = new ApiResponse<Teacherworkhistory>((int)HttpStatusCode.Created, "Tạo mới thành công", created);
            return CreatedAtAction(nameof(GetById), new { id = created.Workhistoryid }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Teacherworkhistory history)
        {
            if (id != history.Workhistoryid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "ID không khớp", null));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }
            var result = await _service.UpdateAsync(id, history);
            if (result == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử công tác với ID = {id}", null));
            }
            return Ok(new ApiResponse<Teacherworkhistory>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử công tác với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}