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
    public class TeacherWorkStatusHistoriesController : ControllerBase
    {
        private readonly ITeacherWorkStatusHistoryService _service;
        public TeacherWorkStatusHistoriesController(ITeacherWorkStatusHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<TeacherWorkStatusHistoryDto>>((int)HttpStatusCode.OK, "Lấy danh sách thành công", data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử trạng thái với ID = {id}", null));
            }
            return Ok(new ApiResponse<TeacherWorkStatusHistoryDto>((int)HttpStatusCode.OK, "Lấy dữ liệu thành công", data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherWorkStatusHistoryRequestDto historyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }
            try
            {
                var createdDto = await _service.CreateAsync(historyDto); 
                var response = new ApiResponse<TeacherWorkStatusHistoryDto>((int)HttpStatusCode.Created, "Tạo mới và cập nhật trạng thái giáo viên thành công", createdDto);
                return CreatedAtAction(nameof(GetById), new { id = createdDto.Historyid }, response);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
            catch (Exception ex)
            {
                // Lỗi server chung
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherWorkStatusHistoryRequestDto historyDto) // Thay đổi tham số
        {
            // Không cần kiểm tra id != history.Historyid vì Historyid không có trong Request DTO
            // id sẽ được lấy từ URL.

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }
            try
            {
                var result = await _service.UpdateAsync(id, historyDto); // Gọi service với DTO request
                if (result == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử trạng thái với ID = {id}", null));
                }
                return Ok(new ApiResponse<TeacherWorkStatusHistoryDto>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
            catch (Exception ex)
            {
                // Lỗi server chung
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử trạng thái với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}