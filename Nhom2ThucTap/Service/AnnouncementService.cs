using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly AppDbContext _context;

        public AnnouncementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Announcement>, int)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Announcements
                .Include(a => a.Creator)
                .OrderByDescending(a => a.Createdat);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<Announcement?> GetByIdAsync(int id)
        {
            return await _context.Announcements
                .Include(a => a.Creator)
                .FirstOrDefaultAsync(a => a.Announcementid == id);
        }

        public async Task<(bool isSuccess, string message)> CreateAsync(Announcement announcement)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Userid == announcement.Creatorid);
            if (!userExists)
                return (false, "Người tạo không tồn tại");

            announcement.Createdat = DateTime.UtcNow;
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
            return (true, "Tạo thông báo thành công");
        }

        public async Task<(bool isSuccess, string message)> UpdateAsync(int id, Announcement announcement)
        {
            var existing = await _context.Announcements.FindAsync(id);
            if (existing == null)
                return (false, "Không tìm thấy thông báo");

            existing.Title = announcement.Title;
            existing.Body = announcement.Body;
            existing.Targetaudience = announcement.Targetaudience;
            existing.Url = announcement.Url;

            await _context.SaveChangesAsync();
            return (true, "Cập nhật thành công");
        }

        public async Task<(bool isSuccess, string message)> DeleteAsync(int id)
        {
            var existing = await _context.Announcements.FindAsync(id);
            if (existing == null)
                return (false, "Không tìm thấy thông báo");

            _context.Announcements.Remove(existing);
            await _context.SaveChangesAsync();
            return (true, "Xoá thành công");
        }
    }
}
