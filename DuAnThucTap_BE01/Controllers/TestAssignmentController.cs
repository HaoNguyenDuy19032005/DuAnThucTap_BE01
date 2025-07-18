using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAssignmentController : ControllerBase
    {
        private readonly ITestassignment _service;

        public TestAssignmentController(ITestassignment service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? searchQuery, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var testAssignments = await _service.GetAllAsync(searchQuery, page, pageSize);
                return Ok(new ApiResponse<IEnumerable<TestAssignmentDto>>((int)HttpStatusCode.OK, "Lấy danh sách phân công bài kiểm tra thành công", testAssignments));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var testAssignment = await _service.GetByIdAsync(id);
            if (testAssignment == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy phân công bài kiểm tra với ID = {id}", null));
            }
            return Ok(new ApiResponse<TestAssignmentDto>((int)HttpStatusCode.OK, "Lấy thông tin phân công bài kiểm tra thành công", testAssignment));
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] TestAssignmentRequestDto testAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var created = await _service.CreateAsync(testAssignment);
                return CreatedAtAction(nameof(Get), new { id = created.Assignmentid }, new ApiResponse<TestAssignmentDto>((int)HttpStatusCode.Created, "Tạo phân công bài kiểm tra thành công", created));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(int id, [FromBody] TestAssignmentRequestDto testAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var updated = await _service.UpdateAsync(id, testAssignment);
                if (updated == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy phân công bài kiểm tra với ID = {id}", null));
                }
                return Ok(new ApiResponse<TestAssignmentDto>((int)HttpStatusCode.OK, "Cập nhật phân công bài kiểm tra thành công", updated));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy phân công bài kiểm tra với ID = {id}", null));
                }
                return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa phân công bài kiểm tra thành công", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }
    }
}