using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;
using Nhom2ThucTap.Data;
using System.Text.RegularExpressions;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly AppDbContext _context;

        public UserController(IUserService service, AppDbContext context)
        {
            _service = service;
            _context = context;
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
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Fullname))
                return BadRequest(new { Message = "Họ tên không được để trống" });

            if (user.Fullname.Length > 100)
                return BadRequest(new { Message = "Họ tên không được vượt quá 100 ký tự" });

            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest(new { Message = "Email không được để trống" });

            if (user.Email.Length > 100)
                return BadRequest(new { Message = "Email không được vượt quá 100 ký tự" });

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(user.Email))
                return BadRequest(new { Message = "Email không đúng định dạng" });

            var isDuplicate = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (isDuplicate)
                return BadRequest(new { Message = "Email đã tồn tại" });

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Roleid == user.Roleid);
            if (role == null)
                return BadRequest(new { Message = "Vai trò không tồn tại" });

            if (role.Rolename == "Teacher")
            {
                if (user.Teacherid == null)
                    return BadRequest(new { Message = "Giáo viên phải có TeacherId" });
                user.Studentid = null;
            }
            else if (role.Rolename == "Student")
            {
                if (user.Studentid == null)
                    return BadRequest(new { Message = "Học sinh phải có StudentId" });
                user.Teacherid = null;
            }
            else
            {
                user.Teacherid = null;
                user.Studentid = null;
            }

            var (isSuccess, message) = await _service.CreateAsync(user);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Không tìm thấy người dùng" });

            if (string.IsNullOrWhiteSpace(user.Fullname))
                return BadRequest(new { Message = "Họ tên không được để trống" });

            if (user.Fullname.Length > 100)
                return BadRequest(new { Message = "Họ tên không được vượt quá 100 ký tự" });

            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest(new { Message = "Email không được để trống" });

            if (user.Email.Length > 100)
                return BadRequest(new { Message = "Email không được vượt quá 100 ký tự" });

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(user.Email))
                return BadRequest(new { Message = "Email không đúng định dạng" });

            var isDuplicate = await _context.Users.AnyAsync(u => u.Email == user.Email && u.Userid != id);
            if (isDuplicate)
                return BadRequest(new { Message = "Email đã tồn tại" });

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Roleid == user.Roleid);
            if (role == null)
                return BadRequest(new { Message = "Vai trò không tồn tại" });

            if (role.Rolename == "Teacher")
            {
                if (user.Teacherid == null)
                    return BadRequest(new { Message = "Giáo viên phải có TeacherId" });
                user.Studentid = null;
            }
            else if (role.Rolename == "Student")
            {
                if (user.Studentid == null)
                    return BadRequest(new { Message = "Học sinh phải có StudentId" });
                user.Teacherid = null;
            }
            else
            {
                user.Teacherid = null;
                user.Studentid = null;
            }

            var (isSuccess, message) = await _service.UpdateAsync(id, user);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Không tìm thấy người dùng" });

            var (isSuccess, message) = await _service.DeleteAsync(id);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }
    }
}
