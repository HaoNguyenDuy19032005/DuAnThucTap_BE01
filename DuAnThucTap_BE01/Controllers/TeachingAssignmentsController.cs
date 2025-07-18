using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.DTOs; // Thêm DTO
using DuAnThucTap_BE01.Helpers; // Thêm ApiResponse
using Microsoft.AspNetCore.Mvc;

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
                TeacherName = assignment.Teacher?.Fullname,       // Chỉ map tên
                SubjectName = assignment.Subject?.Subjectname,     // Chỉ map tên
                ClasstypeName = assignment.Classtype?.Classtypename, // Chỉ map tên
                TopicName = assignment.Topic?.Topicname,         // Chỉ map tên
                SchoolyearName = assignment.Schoolyear?.Schoolyearname, // Chỉ map tên
                Teachingstartdate = assignment.Teachingstartdate,
                Teachingenddate = assignment.Teachingenddate,
                Notes = assignment.Notes
            };
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeachingAssignmentDto>>>> GetAll()
        {
            var assignments = await _service.GetAllAsync();
            var dtos = assignments.Select(MapToDto);
            return Ok(new ApiResponse<IEnumerable<TeachingAssignmentDto>> { Success = true, Message = "Lấy danh sách thành công", Data = dtos });
        }

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
                // Lấy lại dữ liệu đầy đủ để có tên
                var result = await _service.GetByIdAsync(created.Assignmentid);
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