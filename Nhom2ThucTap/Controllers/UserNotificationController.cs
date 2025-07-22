//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Nhom2ThucTap.Models;
//using Nhom2ThucTap.Services;
//using Nhom2ThucTap.Data;

//namespace Nhom2ThucTap.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserNotificationController : ControllerBase
//    {
//        private readonly IUserNotificationService _service;
//        private readonly AppDbContext _context;

//        public UserNotificationController(IUserNotificationService service, AppDbContext context)
//        {
//            _service = service;
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10, int? userId = null)
//        {
//            var (notifications, totalCount) = await _service.GetPagedAsync(page, pageSize, userId);
//            var data = notifications.Select(n => new
//            {
//                n.Uniqusernotificationdueid,
//                n.Userid,
//                UserEmail = n.User?.Email,
//                n.Announcementid,
//                AnnouncementTitle = n.Announcement?.Title,
//                n.Isread,
//                n.Readat
//            });

//            return Ok(new { TotalCount = totalCount, Data = data });
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var notification = await _service.GetByIdAsync(id);
//            if (notification == null)
//                return NotFound(new { Message = "Không tìm thấy thông báo" });

//            return Ok(new
//            {
//                notification.Uniqusernotificationdueid,
//                notification.Userid,
//                UserEmail = notification.User?.Email,
//                notification.Announcementid,
//                AnnouncementTitle = notification.Announcement?.Title,
//                notification.Isread,
//                notification.Readat
//            });
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] Usernotification notification)
//        {
//            // Kiểm tra Userid và Announcementid
//            if (notification.Userid <= 0)
//                return BadRequest(new { Message = "Người dùng không hợp lệ" });

//            if (notification.Announcementid <= 0)
//                return BadRequest(new { Message = "Thông báo không hợp lệ" });

//            // Kiểm tra tồn tại
//            var userExists = await _context.Users.AnyAsync(u => u.Userid == notification.Userid);
//            if (!userExists)
//                return BadRequest(new { Message = "Người dùng không tồn tại" });

//            var announcementExists = await _context.Announcements.AnyAsync(a => a.Announcementid == notification.Announcementid);
//            if (!announcementExists)
//                return BadRequest(new { Message = "Thông báo không tồn tại" });

//            // Kiểm tra trùng
//            var isDuplicate = await _context.Usernotifications.AnyAsync(n =>
//                n.Userid == notification.Userid && n.Announcementid == notification.Announcementid);
//            if (isDuplicate)
//                return BadRequest(new { Message = "Người dùng đã có thông báo này" });

//            var (isSuccess, message) = await _service.CreateAsync(notification);
//            if (!isSuccess)
//                return BadRequest(new { Message = message });

//            return Ok(new { Message = message });
//        }

//        [HttpPut("{id}/mark-read")]
//        public async Task<IActionResult> MarkRead(int id, [FromQuery] bool isRead = true)
//        {
//            var (isSuccess, message) = await _service.UpdateAsync(id, isRead);
//            if (!isSuccess)
//                return NotFound(new { Message = message });

//            return Ok(new { Message = message });
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var (isSuccess, message) = await _service.DeleteAsync(id);
//            if (!isSuccess)
//                return NotFound(new { Message = message });

//            return Ok(new { Message = message });
//        }
//    }
//}
