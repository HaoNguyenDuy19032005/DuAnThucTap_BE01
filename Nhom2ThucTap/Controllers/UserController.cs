using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var (users, totalCount) = await _service.GetPagedUsersAsync(page, pageSize);
            var data = users.Select(u => new
            {
                u.Userid,
                u.Fullname,
                u.Email,
                u.Isactive,
                Role = u.Role?.Rolename
            });

            return Ok(new { TotalCount = totalCount, Data = data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "Không tìm thấy người dùng" });

            return Ok(new
            {
                user.Userid,
                user.Fullname,
                user.Email,
                user.Isactive,
                Role = user.Role?.Rolename
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var (isSuccess, message) = await _service.CreateAsync(user);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            var (isSuccess, message) = await _service.UpdateAsync(id, user);
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
