using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var (roles, totalCount) = await _service.GetPagedRolesAsync(page, pageSize);
            return Ok(new { TotalCount = totalCount, Data = roles });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetByIdAsync(id);
            if (role == null)
                return NotFound(new { Message = "Không tìm thấy vai trò" });

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            var (isSuccess, message) = await _service.CreateAsync(role);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Role role)
        {
            var (isSuccess, message) = await _service.UpdateAsync(id, role);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (isSuccess, message) = await _service.DeleteAsync(id);
            if (!isSuccess)
                return NotFound(new { Message = message });

            return Ok(new { Message = message });
        }
    }
}
