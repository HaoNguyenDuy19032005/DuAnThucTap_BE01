// File: Controllers/LessonsController.cs
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLessons(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchQuery = null)
        {
            var pagedResult = await _lessonService.GetAllLessonsAsync(pageNumber, pageSize, searchQuery);
            var response = new ApiResponse<PagedResponse<LessonDto>>((int)HttpStatusCode.OK, "Lấy danh sách buổi học thành công", pagedResult);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy buổi học với ID = {id}", null));
            }
            return Ok(new ApiResponse<LessonDto>((int)HttpStatusCode.OK, "Lấy thông tin buổi học thành công", lesson));
        }

        [HttpGet("by-title/{title}")]
        public async Task<IActionResult> GetLessonByTitle(string title)
        {
            var lesson = await _lessonService.GetLessonByTitleAsync(title);
            if (lesson == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy buổi học với tiêu đề: '{title}'", null));
            }
            return Ok(new ApiResponse<LessonDto>((int)HttpStatusCode.OK, "Lấy thông tin buổi học thành công", lesson));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] LessonRequestDto lessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var createdLesson = await _lessonService.CreateLessonAsync(lessonDto);
            var response = new ApiResponse<Lesson>((int)HttpStatusCode.Created, "Tạo buổi học thành công", createdLesson);
            return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Lessonid }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonRequestDto lessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var updatedLesson = await _lessonService.UpdateLessonAsync(id, lessonDto);
            if (updatedLesson == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy buổi học với ID = {id}", null));
            }
            return Ok(new ApiResponse<Lesson>((int)HttpStatusCode.OK, "Cập nhật buổi học thành công", updatedLesson));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var success = await _lessonService.DeleteLessonAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy buổi học với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa buổi học thành công", null));
        }
    }
}