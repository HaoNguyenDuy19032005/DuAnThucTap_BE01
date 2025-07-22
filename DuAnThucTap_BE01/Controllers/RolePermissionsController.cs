using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionsController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var data = await _rolePermissionService.GetAllAssignmentsAsync(searchQuery, pageNumber, pageSize);
            var response = new ApiResponse<PagedResponse<RolePermissionDto>>((int)HttpStatusCode.OK, "Lấy danh sách thành công.", data);
            return Ok(response);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermission([FromBody] RolePermissionRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            var result = await _rolePermissionService.AssignPermissionToRoleAsync(requestDto);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, result.ErrorMessage, null));
            }

            var response = new ApiResponse<object>((int)HttpStatusCode.Created, "Gán quyền thành công", result.Assignment);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpDelete("remove/{roleId}/{permissionId}")]
        public async Task<IActionResult> RemovePermission(int roleId, int permissionId)
        {
            var success = await _rolePermissionService.RemovePermissionFromRoleAsync(roleId, permissionId);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy phân quyền với RoleID={roleId} và PermissionID={permissionId}", null));
            }

            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa quyền khỏi vai trò thành công.", null));
        }
    }
}