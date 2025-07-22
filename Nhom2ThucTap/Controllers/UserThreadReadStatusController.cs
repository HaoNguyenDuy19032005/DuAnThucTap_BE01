//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Nhom2ThucTap.Models;
//using Nhom2ThucTap.Services;
//using Nhom2ThucTap.Data;

//namespace Nhom2ThucTap.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserThreadReadStatusController : ControllerBase
//    {
//        private readonly IUserThreadReadStatusService _service;
//        private readonly AppDbContext _context;

//        public UserThreadReadStatusController(IUserThreadReadStatusService service, AppDbContext context)
//        {
//            _service = service;
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var data = await _service.GetAllAsync();
//            return Ok(data.Select(d => new
//            {
//                d.Userid,
//                UserEmail = d.User?.Email,
//                d.Threadid,
//                ThreadTitle = d.Thread?.Title,
//                d.Lastreadtimestamp
//            }));
//        }

//        [HttpGet("{userId}/{threadId}")]
//        public async Task<IActionResult> GetById(int userId, int threadId)
//        {
//            var record = await _service.GetByIdAsync(userId, threadId);
//            if (record == null)
//                return NotFound(new { Message = "Không tìm thấy bản ghi" });

//            return Ok(new
//            {
//                record.Userid,
//                UserEmail = record.User?.Email,
//                record.Threadid,
//                ThreadTitle = record.Thread?.Title,
//                record.Lastreadtimestamp
//            });
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] Userthreadreadstatus entity)
//        {
//            if (entity.Userid <= 0 || entity.Threadid <= 0)
//                return BadRequest(new { Message = "Userid và Threadid phải hợp lệ" });

//            var userExists = await _context.Users.AnyAsync(u => u.Userid == entity.Userid);
//            if (!userExists)
//                return BadRequest(new { Message = "Người dùng không tồn tại" });

//            var threadExists = await _context.Qnathreads.AnyAsync(t => t.Threadid == entity.Threadid);
//            if (!threadExists)
//                return BadRequest(new { Message = "Chủ đề không tồn tại" });

//            var isDuplicate = await _context.Userthreadreadstatuses.AnyAsync(s =>
//                s.Userid == entity.Userid && s.Threadid == entity.Threadid);
//            if (isDuplicate)
//                return BadRequest(new { Message = "Đã tồn tại trạng thái đọc cho người dùng này và chủ đề này" });

//            var (isSuccess, message) = await _service.CreateAsync(entity);
//            if (!isSuccess)
//                return BadRequest(new { Message = message });

//            return Ok(new { Message = message });
//        }

//        [HttpPut("{userId}/{threadId}")]
//        public async Task<IActionResult> Update(int userId, int threadId, [FromQuery] DateTime? lastRead = null)
//        {
//            var record = await _service.GetByIdAsync(userId, threadId);
//            if (record == null)
//                return NotFound(new { Message = "Không tìm thấy bản ghi cần cập nhật" });

//            var (isSuccess, message) = await _service.UpdateAsync(userId, threadId, lastRead);
//            if (!isSuccess)
//                return BadRequest(new { Message = message });

//            return Ok(new { Message = message });
//        }

//        [HttpDelete("{userId}/{threadId}")]
//        public async Task<IActionResult> Delete(int userId, int threadId)
//        {
//            var (isSuccess, message) = await _service.DeleteAsync(userId, threadId);
//            if (!isSuccess)
//                return NotFound(new { Message = message });

//            return Ok(new { Message = message });
//        }
//    }
//}
