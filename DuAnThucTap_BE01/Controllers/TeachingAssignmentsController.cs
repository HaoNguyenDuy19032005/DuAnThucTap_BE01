using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.DTOs;
using DuAnThucTap_BE01.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachingAssignmentsController : ControllerBase
    {
        private readonly ITeachingAssignmentService _service;

        public TeachingAssignmentsController(ITeachingAssignmentService service)
        {
            _service = service;
        }

        // Helper để map từ Entity sang DTO
        private TeachingAssignmentDto MapToDto(Teachingassignment assignment)
        {
            return new TeachingAssignmentDto
            {
                Assignmentid = assignment.Assignmentid,
                TeacherName = assignment.Teacher?.Fullname,
                SubjectName = assignment.Subject?.Subjectname,
                ClasstypeName = assignment.Classtype?.Classtypename,
                TopicName = assignment.Topic?.Topicname,
                SchoolyearName = assignment.Schoolyear?.Schoolyearname,
                Teachingstartdate = assignment.Teachingstartdate,
                Teachingenddate = assignment.Teachingenddate,
                Notes = assignment.Notes
            };
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<TeachingAssignmentDto>>>> GetAll(
            [FromQuery] string? searchTerm,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            // Gọi service với các tham số phân trang và tìm kiếm
            var pagedResult = await _service.GetAllAsync(searchTerm, pageNumber, pageSize);

            // Chuyển đổi danh sách Items từ Entity sang DTO
            var dtos = pagedResult.Items.Select(MapToDto).ToList();

            // Tạo một PagedResult mới chứa danh sách DTO
            var pagedDtoResult = new PagedResult<TeachingAssignmentDto>
            {
                Items = dtos,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalCount = pagedResult.TotalCount
            };

            // Trả về kết quả trong ApiResponse
            return Ok(new ApiResponse<PagedResult<TeachingAssignmentDto>>
            {
                Success = true,
                Message = "Lấy danh sách phân công thành công",
                Data = pagedDtoResult
            });
        }

        // --- Các action khác (GetById, Create, Update, Delete) giữ nguyên ---
        // (Tôi đã cập nhật GetById để nó cũng trả về đủ tên)
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TeachingAssignmentDto>>> GetById(int id)
        {
            var assignment = await _service.GetByIdAsync(id);
            if (assignment == null)
            {
                return NotFound(new ApiResponse<TeachingAssignmentDto> { Success = false, Message = "Không tìm thấy phân công." });
            }
            var dto = MapToDto(assignment);
            return Ok(new ApiResponse<TeachingAssignmentDto> { Success = true, Message = "Lấy dữ liệu thành công", Data = dto });
        }

        // ... (Create, Update, Delete actions)
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TeachingAssignmentDto>>> Create([FromBody] Teachingassignment teachingAssignment)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ApiResponse<object> { Success = false, Message = string.Join(" ", errors) });
            }

            try
            {
                var created = await _service.CreateAsync(teachingAssignment);
                var result = await _service.GetByIdAsync(created.Assignmentid); // Lấy lại dữ liệu đầy đủ
                var dto = MapToDto(result);
                return Ok(new ApiResponse<TeachingAssignmentDto> { Success = true, Message = "Thêm phân công thành công!", Data = dto });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TeachingAssignmentDto>>> Update(int id, [FromBody] Teachingassignment teachingAssignment)
        {
            if (id != teachingAssignment.Assignmentid)
                return BadRequest(new ApiResponse<object> { Success = false, Message = "ID không khớp." });

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ApiResponse<object> { Success = false, Message = string.Join(" ", errors) });
            }

            try
            {
                var updated = await _service.UpdateAsync(id, teachingAssignment);
                if (updated == null)
                {
                    return NotFound(new ApiResponse<object> { Success = false, Message = "Không tìm thấy phân công để cập nhật." });
                }
                var result = await _service.GetByIdAsync(updated.Assignmentid);
                var dto = MapToDto(result);
                return Ok(new ApiResponse<TeachingAssignmentDto> { Success = true, Message = "Cập nhật thành công!", Data = dto });
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
                return NotFound(new ApiResponse<object> { Success = false, Message = "Không tìm thấy phân công để xóa." });
            }
            return Ok(new ApiResponse<object> { Success = true, Message = "Xóa thành công." });
        }
    }
}
