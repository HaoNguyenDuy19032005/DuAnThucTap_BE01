using DuAnThucTap_BE01.DTO;
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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? searchQuery, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var permissions = await _service.GetAllAsync(searchQuery, page, pageSize);
                return Ok(new ApiResponse<IEnumerable<PermissionDto>>((int)HttpStatusCode.OK, "Lấy danh sách quyền thành công", permissions));
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
            var permission = await _service.GetByIdAsync(id);
            if (permission == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quyền với ID = {id}", null));
            }
            return Ok(new ApiResponse<PermissionDto>((int)HttpStatusCode.OK, "Lấy thông tin quyền thành công", permission));
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] PermissionRequestDto permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var created = await _service.CreateAsync(permission);
                return CreatedAtAction(nameof(Get), new { id = created.PermissionId }, new ApiResponse<PermissionDto>((int)HttpStatusCode.Created, "Tạo quyền thành công", created));
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
        public async Task<IActionResult> Update(int id, [FromBody] PermissionRequestDto permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var updated = await _service.UpdateAsync(id, permission);
                if (updated == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quyền với ID = {id}", null));
                }
                return Ok(new ApiResponse<PermissionDto>((int)HttpStatusCode.OK, "Cập nhật quyền thành công", updated));
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
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quyền với ID = {id}", null));
                }
                return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa quyền thành công", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }
    }
}