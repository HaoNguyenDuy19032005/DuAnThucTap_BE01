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
    public class TeacherTrainingHistoriesController : ControllerBase
    {
        private readonly ITeacherTrainingHistoryService _service;
        public TeacherTrainingHistoriesController(ITeacherTrainingHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<TeacherTrainingHistoryDto>>((int)HttpStatusCode.OK, "Lấy danh sách thành công", data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử đào tạo với ID = {id}", null));
            }
            return Ok(new ApiResponse<TeacherTrainingHistoryDto>((int)HttpStatusCode.OK, "Lấy dữ liệu thành công", data));
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] TeacherTrainingHistoryRequestDto historyDto) // Thay đổi tham số
        //{
        //    // ModelState.IsValid sẽ tự động kiểm tra các Data Annotations trong TeacherTrainingHistoryRequestDto
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
        //    }

        //    var created = await _service.CreateAsync(historyDto); // Gọi service với DTO request
        //    var response = new ApiResponse<Teachertraininghistory>((int)HttpStatusCode.Created, "Tạo mới thành công", created);
        //    return CreatedAtAction(nameof(GetById), new { id = created.Trainingid }, response);

        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] TeacherTrainingHistoryRequestDto historyDto) // Thay đổi tham số
        //{
        //    // Không cần kiểm tra id != history.Trainingid vì Trainingid không có trong Request DTO
        //    // id sẽ được lấy từ URL.

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
        //    }

        //    var result = await _service.UpdateAsync(id, historyDto); // Gọi service với DTO request
        //    if (result == null)
        //    {
        //        return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử đào tạo với ID = {id}", null));
        //    }
        //    return Ok(new ApiResponse<Teachertraininghistory>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử đào tạo với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}
