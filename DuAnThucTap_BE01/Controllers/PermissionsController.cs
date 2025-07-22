using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Lấy danh sách tất cả các quyền (hỗ trợ tìm kiếm và phân trang).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var data = await _permissionService.GetAllPermissionsAsync(searchQuery, pageNumber, pageSize);
            var response = new ApiResponse<PagedResponse<PermissionDto>>((int)HttpStatusCode.OK, "Lấy danh sách quyền thành công", data);
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin một quyền theo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _permissionService.GetPermissionByIdAsync(id);
            if (data == null)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quyền với ID = {id}", null));
            }
            return Ok(new ApiResponse<PermissionDto>((int)HttpStatusCode.OK, "Lấy dữ liệu thành công", data));
        }

        /// <summary>
        /// Tạo một quyền mới.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermissionRequestDto permissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var created = await _permissionService.CreatePermissionAsync(permissionDto);
                var response = new ApiResponse<Permission>((int)HttpStatusCode.Created, "Tạo mới thành công", created);
                return CreatedAtAction(nameof(GetById), new { id = created.Permissionid }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        /// <summary>
        /// Cập nhật thông tin một quyền.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionRequestDto permissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ", ModelState));
            }

            try
            {
                var result = await _permissionService.UpdatePermissionAsync(id, permissionDto);
                if (result == null)
                {
                    return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quyền với ID = {id}", null));
                }
                return Ok(new ApiResponse<Permission>((int)HttpStatusCode.OK, "Cập nhật thành công", result));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        /// <summary>
        /// Xóa một quyền theo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _permissionService.DeletePermissionAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>((int)HttpStatusCode.NotFound, $"Không tìm thấy quyền với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>((int)HttpStatusCode.OK, "Xóa thành công", null));
        }
    }
}