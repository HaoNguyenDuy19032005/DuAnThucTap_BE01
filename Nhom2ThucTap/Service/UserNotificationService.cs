using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly AppDbContext _context;

        public UserNotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Usernotification> Notifications, int TotalCount)> GetPagedAsync(int page, int pageSize, int? userId = null)
        {
            var query = _context.Usernotifications
                .Include(n => n.User)
                .Include(n => n.Announcement)
                .AsQueryable();

            if (userId.HasValue)
                query = query.Where(n => n.Userid == userId);

            var totalCount = await query.CountAsync();
            var notifications = await query
                .OrderByDescending(n => n.Uniqusernotificationdueid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (notifications, totalCount);
        }

        public async Task<Usernotification?> GetByIdAsync(int id)
        {
            return await _context.Usernotifications
                .Include(n => n.User)
                .Include(n => n.Announcement)
                .FirstOrDefaultAsync(n => n.Uniqusernotificationdueid == id);
        }

        public async Task<(bool IsSuccess, string Message)> CreateAsync(Usernotification notification)
        {
            if (notification.Userid <= 0)
                return (false, "UserID không hợp lệ");
            if (notification.Announcementid <= 0)
                return (false, "AnnouncementID không hợp lệ");

            notification.Isread ??= false;
            notification.Readat = null;

            _context.Usernotifications.Add(notification);
            await _context.SaveChangesAsync();

            return (true, "Tạo thông báo thành công");
        }

        public async Task<(bool IsSuccess, string Message)> UpdateAsync(int id, bool isRead)
        {
            var existing = await _context.Usernotifications.FindAsync(id);
            if (existing == null)
                return (false, "Không tìm thấy thông báo");

            existing.Isread = isRead;
            existing.Readat = isRead ? DateTime.UtcNow : null;

            await _context.SaveChangesAsync();
            return (true, "Cập nhật trạng thái thông báo thành công");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteAsync(int id)
        {
            var existing = await _context.Usernotifications.FindAsync(id);
            if (existing == null)
                return (false, "Không tìm thấy thông báo");

            _context.Usernotifications.Remove(existing);
            await _context.SaveChangesAsync();
            return (true, "Xóa thông báo thành công");
        }
    }
}
