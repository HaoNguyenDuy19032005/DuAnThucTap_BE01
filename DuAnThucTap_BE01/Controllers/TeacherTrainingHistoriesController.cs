using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http;

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

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
        [FromForm] TeacherTrainingHistoryRequestDto historyDto,
        [FromForm] IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            if (file != null && file.Length > 5 * 1024 * 1024)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Kích thước tệp không được vượt quá 5MB", null));
            }

            var created = await _service.CreateAsync(historyDto, file);
            var response = new ApiResponse<Teachertraininghistory>((int)HttpStatusCode.Created, "Tạo mới thành công", created);
            return CreatedAtAction(nameof(GetById), new { id = created.Trainingid }, response);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")] // Specify that this endpoint accepts multipart/form-data
        public async Task<IActionResult> Update(
            int id,
            [FromForm] TeacherTrainingHistoryRequestDto historyDto, // Use FromForm for form-data
            [FromForm] IFormFile? file) // Add file parameter
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            // Optional: Validate file (e.g., size, type)
            if (file != null && file.Length > 5 * 1024 * 1024) // Example: Limit file size to 5MB
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Kích thước tệp không được vượt quá 5MB", null));
            }

            var result = await _service.UpdateAsync(id, historyDto, file); // Pass file to service
            if (result == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy lịch sử đào tạo với ID = {id}", null));
            }
            return Ok(new ApiResponse<Teachertraininghistory>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
        }

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