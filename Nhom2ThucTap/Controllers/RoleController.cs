using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        private readonly AppDbContext _context;

        public RoleController(IRoleService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        // GET: api/Role?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var (roles, totalCount) = await _service.GetPagedRolesAsync(page, pageSize);
            return Ok(new { TotalCount = totalCount, Data = roles });
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetByIdAsync(id);
            if (role == null)
                return NotFound(new { Message = "Không tìm thấy vai trò" });

            return Ok(role);
        }

        // POST: api/Role
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Rolename))
                return BadRequest(new { Message = "Tên vai trò không được để trống" });

            if (role.Rolename.Length > 100)
                return BadRequest(new { Message = "Tên vai trò không được vượt quá 100 ký tự" });

            var isDuplicate = await _context.Roles.AnyAsync(r => r.Rolename == role.Rolename);
            if (isDuplicate)
                return BadRequest(new { Message = "Tên vai trò đã tồn tại" });

            var (isSuccess, message) = await _service.CreateAsync(role);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Role role)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Không tìm thấy vai trò" });

            if (string.IsNullOrWhiteSpace(role.Rolename))
                return BadRequest(new { Message = "Tên vai trò không được để trống" });

            if (role.Rolename.Length > 100)
                return BadRequest(new { Message = "Tên vai trò không được vượt quá 100 ký tự" });

            var isDuplicate = await _context.Roles.AnyAsync(r => r.Rolename == role.Rolename && r.Roleid != id);
            if (isDuplicate)
                return BadRequest(new { Message = "Tên vai trò đã tồn tại" });

            var (isSuccess, message) = await _service.UpdateAsync(id, role);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Không tìm thấy vai trò" });

            var (isSuccess, message) = await _service.DeleteAsync(id);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }
    }
}
