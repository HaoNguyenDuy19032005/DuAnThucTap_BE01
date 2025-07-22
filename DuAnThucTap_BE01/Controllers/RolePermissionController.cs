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
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _service;

        public RolePermissionController(IRolePermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? searchQuery, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var rolePermissions = await _service.GetAllAsync(searchQuery, page, pageSize);
                return Ok(new ApiResponse<IEnumerable<RolePermissionDto>>((int)HttpStatusCode.OK, "Lấy danh sách quan hệ vai trò-quyền thành công", rolePermissions));
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

        [HttpGet("{roleId}/{permissionId}")]
        public async Task<IActionResult> Get(int roleId, int permissionId)
        {
            var rolePermission = await _service.GetByIdAsync(roleId, permissionId);
            if (rolePermission == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quan hệ vai trò-quyền với RoleID = {roleId} và PermissionID = {permissionId}", null));
            }
            return Ok(new ApiResponse<RolePermissionDto>((int)HttpStatusCode.OK, "Lấy thông tin quan hệ vai trò-quyền thành công", rolePermission));
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] RolePermissionRequestDto rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var created = await _service.CreateAsync(rolePermission);
                return CreatedAtAction(nameof(Get), new { roleId = created.RoleId, permissionId = created.PermissionId }, new ApiResponse<RolePermissionDto>((int)HttpStatusCode.Created, "Tạo quan hệ vai trò-quyền thành công", created));
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

        [HttpDelete("{roleId}/{permissionId}")]
        public async Task<IActionResult> Delete(int roleId, int permissionId)
        {
            try
            {
                var deleted = await _service.DeleteAsync(roleId, permissionId);
                if (!deleted)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quan hệ vai trò-quyền với RoleID = {roleId} và PermissionID = {permissionId}", null));
                }
                return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa quan hệ vai trò-quyền thành công", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(500, "Đã có lỗi xảy ra trong quá trình xử lý.", ex.Message));
            }
        }
    }
}