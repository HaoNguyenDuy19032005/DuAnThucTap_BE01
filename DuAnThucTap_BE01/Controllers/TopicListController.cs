using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.DTOs;      // Thêm DTO
using DuAnThucTap_BE01.Helpers;  // Thêm ApiResponse
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicListController : ControllerBase
    {
        private readonly ITopicListService _service;

        public TopicListController(ITopicListService service)
        {
            _service = service;
        }

        // Helper để map sang DTO
        private TopicListDto MapToDto(Topiclist topic)
        {
            return new TopicListDto
            {
                Topicid = topic.Topicid,
                Topicname = topic.Topicname,
                Description = topic.Description,
                Teachingenddate = topic.Teachingenddate
            };
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TopicListDto>>>> GetAll()
        {
            var topics = await _service.GetAllAsync();
            var dtos = topics.Select(MapToDto);
            return Ok(new ApiResponse<IEnumerable<TopicListDto>> { Success = true, Message = "Lấy danh sách chủ đề thành công", Data = dtos });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TopicListDto>>> GetById(int id)
        {
            var topic = await _service.GetByIdAsync(id);
            if (topic == null)
            {
                return NotFound(new ApiResponse<TopicListDto> { Success = false, Message = "Không tìm thấy chủ đề." });
            }
            return Ok(new ApiResponse<TopicListDto> { Success = true, Data = MapToDto(topic) });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TopicListDto>>> Create([FromBody] Topiclist topicList)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ApiResponse<object> { Success = false, Message = string.Join(" ", errors) });
            }
            try
            {
                var created = await _service.CreateAsync(topicList);
                return Ok(new ApiResponse<TopicListDto> { Success = true, Message = "Tạo chủ đề thành công!", Data = MapToDto(created) });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object> { Success = false, Message = ex.Message });
            }
        }

        // Tương tự cho Update và Delete
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TopicListDto>>> Update(int id, [FromBody] Topiclist topicList)
        {
            if (id != topicList.Topicid)
                return BadRequest(new ApiResponse<object> { Success = false, Message = "ID không khớp." });

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ApiResponse<object> { Success = false, Message = string.Join(" ", errors) });
            }
            try
            {
                var result = await _service.UpdateAsync(id, topicList);
                if (result == null)
                    return NotFound(new ApiResponse<object> { Success = false, Message = "Không tìm thấy chủ đề." });

                return Ok(new ApiResponse<TopicListDto> { Success = true, Message = "Cập nhật thành công!", Data = MapToDto(result) });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object> { Success = false, Message = "Không tìm thấy chủ đề để xóa." });
            }
            return Ok(new ApiResponse<object> { Success = true, Message = "Xóa thành công." });
        }
    }
}