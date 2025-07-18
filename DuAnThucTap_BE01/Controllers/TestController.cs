using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITests _service;

        public TestController(ITests service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? searchQuery, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Trang hoặc kích thước trang không hợp lệ.", null));
            }

            var tests = await _service.GetAllAsync(searchQuery, page, pageSize);
            return Ok(new ApiResponse<IEnumerable<TestDto>>((int)HttpStatusCode.OK, "Lấy danh sách bài kiểm tra thành công", tests));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var test = await _service.GetByIdAsync(id);
            if (test == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy bài kiểm tra với ID = {id}", null));
            }
            return Ok(new ApiResponse<TestDto>((int)HttpStatusCode.OK, "Lấy thông tin bài kiểm tra thành công", test));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] TestRequestDto testDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var created = await _service.CreateAsync(testDto);
                var response = new ApiResponse<TestDto>((int)HttpStatusCode.Created, "Tạo bài kiểm tra thành công", created);
                return CreatedAtAction(nameof(Get), new { id = created.Testid }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] TestRequestDto testDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var updated = await _service.UpdateAsync(id, testDto);
                if (updated == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy bài kiểm tra với ID = {id}", null));
                }
                return Ok(new ApiResponse<TestDto>((int)HttpStatusCode.OK, "Cập nhật bài kiểm tra thành công", updated));
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
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy bài kiểm tra với ID = {id}", null));
                }
                return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa bài kiểm tra thành công", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }
    }
}       